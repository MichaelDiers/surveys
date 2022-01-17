"""Google cloud function that is triggered if a new survey document is created in Firebase"""
# pylint: disable=broad-except

import os
import sys

from google.cloud import pubsub_v1

publisher = pubsub_v1.PublisherClient()

ENV_PROJECT_ID = os.environ['ENV_PROJECT_ID']
ENV_TEMPLATE_NEWLINE = os.environ['ENV_TEMPLATE_NEWLINE']

ENV_TOPIC_SURVEY_STATUS_UPDATE = os.environ['ENV_TOPIC_SURVEY_STATUS_UPDATE']
ENV_TOPIC_SURVEY_STATUS_UPDATE_FORMAT = \
    '{{"id":"{f_survey_id}", "type":"SURVEY", "status":"{f_survey_status}"}}'
ENV_SURVEY_STATUS_INVITATION_MAILS_REQUEST_FAILED = \
    os.environ['ENV_SURVEY_STATUS_INVITATION_MAILS_REQUEST_FAILED']
ENV_SURVEY_STATUS_INVITATION_MAILS_REQUEST_OK = \
    os.environ['ENV_SURVEY_STATUS_INVITATION_MAILS_REQUEST_OK']

ENV_TOPIC_SEND_MAIL =  os.environ['ENV_TOPIC_SEND_MAIL']
ENV_SURVEY_VIEWER_LINK = os.environ['ENV_SURVEY_VIEWER_LINK']

SEND_MAIL_PUB_FORMAT = """{{
    "recipients": [
        {{            
            "email": "{f_recipient_email}",
            "name": "{f_recipient_name}"
        }}
    ],
    "replyTo": {{
        "email": "{f_organizer_email}",
        "name": "{f_organizer_name}"
    }},
    "subject": "{f_subject}",
    "text": {{
        "html": "{f_text_html}",
        "plain": "{f_text_plain}"
    }}
}}"""

MAIL_PLAIN_FORMAT = "Hej {f_participant_name},{f_newline}{f_newline}eine neue Umfrage '{f_survey_name}' steht für dich bereit:{f_newline}{f_newline}{f_survey_link}{f_participant_id}{f_newline}{f_newline}Viele Grüße,{f_newline}{f_newline}{f_organizer_name}{f_newline}"

MAIL_HTML_FORMAT = "<html><body><h1>Hej {f_participant_name}!</h1><p>Eine neue Umfrage <a href='{f_survey_link}{f_participant_id}'>{f_survey_name}</a> steht für doch bereit!</p><p>Viele Grüße,<br><br>{f_organizer_name}</p></body><html>"

MAIL_SUBJECT_FORMAT = 'Neue Umfrage {f_survey_name}'

def send_survey_invitations(survey):
    """
    Creates invitation emails in html and text format and sends a request to
    Pub/Sub to send the emails to the participants.
    Args:
        - survey (dict): The Firebase document of the survey in a data type enriched version.
    Returns:
        True if are mails are requested and False otherwise.
    """
    survey_name = survey['name']['stringValue']

    organizer_email = survey['organizer']['mapValue']['fields']['email']['stringValue']
    organizer_name = survey['organizer']['mapValue']['fields']['name']['stringValue']

    subject = MAIL_SUBJECT_FORMAT.format(f_survey_name=survey_name)

    # pylint: disable-next=no-member
    topic_path = publisher.topic_path(ENV_PROJECT_ID, ENV_TOPIC_SEND_MAIL)

    success = True
    participant_ids = survey['participantIds']['arrayValue']['values']
    for participant_id in [value['stringValue'] for value in participant_ids]:
        participant_fields = survey[participant_id]['mapValue']['fields']
        participant_email = participant_fields['email']['stringValue']
        participant_name = participant_fields['name']['stringValue']

        mail_html = MAIL_HTML_FORMAT.format(
            f_participant_name=participant_name,
            f_survey_name=survey_name,
            f_survey_link=ENV_SURVEY_VIEWER_LINK,
            f_participant_id=participant_email,
            f_organizer_name=organizer_name)
        mail_plain = MAIL_PLAIN_FORMAT.format(
            f_participant_name=participant_name,
            f_survey_name=survey_name,
            f_survey_link=ENV_SURVEY_VIEWER_LINK,
            f_participant_id=participant_id,
            f_organizer_name=organizer_name,
            f_newline="###TEMPLATE_BR###")

        message = SEND_MAIL_PUB_FORMAT.format(
            f_recipient_email=organizer_email,
            f_recipient_name=organizer_name,
            f_organizer_email=organizer_email,
            f_organizer_name=organizer_name,
            f_subject=subject,
            f_text_html=mail_html,
            f_text_plain=mail_plain
        )

        message_bytes = message.encode('utf-8')

        try:
            publish_future = publisher.publish(topic_path, data=message_bytes)
            publish_future.result()
        except Exception as error:
            success = False
            print(
                f'Pub/Sub request error: Topic path: {str(topic_path)}, Message: {message}',
                file=sys.stderr)
            print(str(error), file=sys.stderr)

        return success

def update_survey_status(survey_id, survey_status):
    """
    Send a request to Pub/Sub to update the status of a survey.
    Args:
        survey_id (string): The id of the survey.
        survey_status (string): The new status of the survey.
    """
    message = ENV_TOPIC_SURVEY_STATUS_UPDATE_FORMAT.format(
        f_survey_id = survey_id,
        f_survey_status=survey_status)
    message_bytes = message.encode('utf-8')

    # pylint: disable-next=no-member
    topic_path = publisher.topic_path(ENV_PROJECT_ID, ENV_TOPIC_SURVEY_STATUS_UPDATE)

    try:
        publish_future = publisher.publish(topic_path, data=message_bytes)
        publish_future.result()
    except Exception as error:
        print(
            f'Pub/Sub request error: Topic path: {str(topic_path)}, Message: {message}',
            file=sys.stderr)
        print(str(error), file=sys.stderr)

# pylint: disable-next=unused-argument
def on_survey_created(data, context):
    """
    Triggered by a create operation of a survey Firestore document.
    Requests are sent to Pub/Sub
        - request to send invitation emails to the survey participants
        - request to update the status of the survey
    Args:
        data (dict): The event payload.
        context (google.cloud.functions.Context): Metadata for the event.
    Remarks:
        Access the fields of the created document:
            data['value']['fields']

        The event payload includes a data type enriched version of the survey document.
        Example:
            Firestore document: { "name": "My survey name", ... }
            Payload:            { "name": { "stringValue": "My survey name" }, ... }
    """
    try:
        survey = data["value"]
        survey_id = survey["name"].split("/")[-1]
        survey = survey['fields']
        print(survey)

        new_survey_status = ENV_SURVEY_STATUS_INVITATION_MAILS_REQUEST_OK

        # send an invitation email to the participants
        if not send_survey_invitations(survey):
            new_survey_status = ENV_SURVEY_STATUS_INVITATION_MAILS_REQUEST_FAILED

        # set the new survey status
        update_survey_status(survey_id, new_survey_status)
    except Exception as error:
        print(f'Global error: {str(error)}', file=sys.stderr)
