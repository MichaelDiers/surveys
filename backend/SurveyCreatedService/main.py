import base64
import json
import os
import sys

from google.cloud import pubsub_v1

ENV_PROJECT_ID = 'ENV_PROJECT_ID'
ENV_TOPIC_NAME_SEND_MAIL = 'ENV_TOPIC_NAME_SEND_MAIL'

PARTICIPANTS = 'participants'
PARTICIPANT_NAME = 'name'
PARTICIPANT_EMAIL = 'email'
SURVEY_NAME = 'name'

MESSAGE_EMAIL_TYPE = 'emailType'
MESSAGE_EMAIL_TYPE_VALUE = 'surveyRequest'
MESSAGE_RECIPIENTS = 'recipients'
MESSAGE_RECIPIENT_NAME = 'name'
MESSAGE_RECIPIENT_EMAIL = 'email'
MESSAGE_SURVEY_LINK = 'surveyLink'
MESSAGE_SURVEY_NAME = 'surveyName'

publisher = pubsub_v1.PublisherClient()

def create_message(surveyName, participant):
    return {
                MESSAGE_EMAIL_TYPE: MESSAGE_EMAIL_TYPE_VALUE,
                MESSAGE_RECIPIENTS: [
                    {
                        MESSAGE_RECIPIENT_NAME: participant[PARTICIPANT_NAME],
                        MESSAGE_RECIPIENT_EMAIL:participant[PARTICIPANT_EMAIL]
                    }
                ],
                MESSAGE_SURVEY_LINK: "",
                MESSAGE_SURVEY_NAME: surveyName
    }

def send_mails(survey):
    survey_name = survey[SURVEY_NAME]

    project_id = os.environ.get(ENV_PROJECT_ID, f'Specified environment variable is not set: {ENV_PROJECT_ID}')
    topic_name_send_mail = os.environ.get(ENV_TOPIC_NAME_SEND_MAIL, f'Specified environment variable is not set: {ENV_TOPIC_NAME_SEND_MAIL}')
    topic_path_send_mail = publisher.topic_path(project_id, topic_name_send_mail)

    for participant in survey[PARTICIPANTS]:
        message = create_message(survey_name, participant)
        message_json = json.dumps(message)
        message_bytes = message_json.encode('utf-8')
        publish_future = publisher.publish(topic_path_send_mail, data=message_bytes)
        publish_future.result()

def validate(survey):
    for name in [PARTICIPANTS, SURVEY_NAME]:
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
        else:
            print('No data!', file = sys.stderr)
    except Exception as error:
        print(f'Unexpected error: {str(error)}', file = sys.stderr)
