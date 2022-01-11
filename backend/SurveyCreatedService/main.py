import base64
import json
import os
import sys

from google.cloud import pubsub_v1

ENV_PROJECT_ID = 'ENV_PROJECT_ID'
ENV_TOPIC_NAME_SEND_MAIL = 'ENV_TOPIC_NAME_SEND_MAIL'
ENV_TOPIC_NAME_UPDATE_SURVEY = 'ENV_TOPIC_NAME_UPDATE_SURVEY'
ENV_SURVEY_VIEWER_LINK = 'ENV_SURVEY_VIEWER_LINK'

PARTICIPANTS = 'participants'
PARTICIPANT_NAME = 'name'
PARTICIPANT_EMAIL = 'email'
PARTICIPANT_GUID = 'guid'
SURVEY_NAME = 'name'
REPLY_TO_EMAIL = 'replyToEmail'

MESSAGE_EMAIL_TYPE = 'emailType'
MESSAGE_EMAIL_TYPE_VALUE = 'surveyRequest'
MESSAGE_RECIPIENTS = 'recipients'
MESSAGE_RECIPIENT_NAME = 'name'
MESSAGE_RECIPIENT_EMAIL = 'email'
MESSAGE_SURVEY_LINK = 'surveyLink'
MESSAGE_SURVEY_NAME = 'surveyName'
MESSAGE_REPLY_TO = 'replyTo'

MESSAGE_UPDATE_SURVEY_ID = 'surveyId'
MESSAGE_UPDATE_SURVEY_TYPE = 'type'
MESSAGE_UPDATE_SURVEY_TYPE_VALUE = 'status'
MESSAGE_UPDATE_SURVEY_STATUS = 'status'
MESSAGE_UPDATE_SURVEY_STATUS_VALUE = 'SEND_MAIL'

publisher = pubsub_v1.PublisherClient()

def create_message_send_mail(surveyName, participant, reply_to):
    link = os.environ.get(ENV_SURVEY_VIEWER_LINK, f'Specified environment variable is not set: {ENV_SURVEY_VIEWER_LINK}')

    return {
                MESSAGE_EMAIL_TYPE: MESSAGE_EMAIL_TYPE_VALUE,
                MESSAGE_RECIPIENTS: [
                    {
                        MESSAGE_RECIPIENT_NAME: participant[PARTICIPANT_NAME],
                        MESSAGE_RECIPIENT_EMAIL:participant[PARTICIPANT_EMAIL]
                    }
                ],
                MESSAGE_SURVEY_LINK: f'{link}{participant[PARTICIPANT_GUID]}',
                MESSAGE_SURVEY_NAME: surveyName,
                MESSAGE_REPLY_TO: reply_to
    }

def send_update_survey(survey_id):
    project_id = os.environ.get(ENV_PROJECT_ID, f'Specified environment variable is not set: {ENV_PROJECT_ID}')
    topic_name = os.environ.get(ENV_TOPIC_NAME_UPDATE_SURVEY, f'Specified environment variable is not set: {ENV_TOPIC_NAME_UPDATE_SURVEY}')
    topic_path = publisher.topic_path(project_id, topic_name)

    message = {
        MESSAGE_UPDATE_SURVEY_ID: survey_id,
        MESSAGE_UPDATE_SURVEY_TYPE: MESSAGE_UPDATE_SURVEY_TYPE_VALUE,
        MESSAGE_UPDATE_SURVEY_STATUS: MESSAGE_UPDATE_SURVEY_STATUS_VALUE
    }

    message_json = json.dumps(message)
    message_bytes = message_json.encode('utf-8')
    publish_future = publisher.publish(topic_path, data=message_bytes)
    publish_future.result()

def send_mails(survey):
    survey_name = survey[SURVEY_NAME]
    reply_to_email = survey[REPLY_TO_EMAIL]

    project_id = os.environ.get(ENV_PROJECT_ID, f'Specified environment variable is not set: {ENV_PROJECT_ID}')
    topic_name_send_mail = os.environ.get(ENV_TOPIC_NAME_SEND_MAIL, f'Specified environment variable is not set: {ENV_TOPIC_NAME_SEND_MAIL}')
    topic_path_send_mail = publisher.topic_path(project_id, topic_name_send_mail)

    for participant in survey[PARTICIPANTS]:
        message = create_message_send_mail(survey_name, participant, reply_to_email)
        message_json = json.dumps(message)
        message_bytes = message_json.encode('utf-8')
        publish_future = publisher.publish(topic_path_send_mail, data=message_bytes)
        publish_future.result()

def validate(survey):
    for name in [PARTICIPANTS, SURVEY_NAME, REPLY_TO_EMAIL]:
        value = survey.get(name)
        if value is None or len(value) == 0:
            return False

    for participant in survey[PARTICIPANTS]:
        if participant == None \
            or PARTICIPANT_NAME not in participant or len(participant[PARTICIPANT_NAME]) == 0 \
            or PARTICIPANT_EMAIL not in participant or len(participant[PARTICIPANT_EMAIL]) == 0:
            return False
    
    return True

def on_survey_created(event, context):
    """Triggered from a message on a Cloud Pub/Sub topic.
    Args:
         event (dict): Event payload.
         context (google.cloud.functions.Context): Metadata for the event.
    """
    try:
        if 'data' in event:
            json_message = base64.b64decode(event['data']).decode('utf-8')
            survey = json.loads(json_message)
            if not validate(survey):
                print(f'Invalid data: {str(json_message)} - {str(survey)}', file = sys.stderr)
            else:
                send_mails(survey)
                send_update_survey(survey['id'])
        else:
            print('No data!', file = sys.stderr)
    except Exception as error:
        print(f'Unexpected error: {str(error)}', file = sys.stderr)
