import base64
import json
import sys

PARTICIPANTS = 'Participants'
PARTICIPANT_NAME = 'Name'
PARTICIPANT_EMAIL = 'Email'
SURVEY_NAME = 'Name'

def create_message(surveyName, participant):
    return {
                "EmailType": "SurveyRequest",
                "Recipients": [
                    {
                        "Name": participant[PARTICIPANT_NAME],
                        "Email":participant[PARTICIPANT_EMAIL]
                    }
                ],
                "SurveyLink": "",
                "SurveyName": surveyName
    }

def send_mails(survey):
    survey_name = survey[SURVEY_NAME]
    for participant in survey[PARTICIPANTS]:
        message = create_message(survey_name, participant)
        print(message)

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
                print(f'Invalid data: {str(json_message)}', file = sys.stderr)
            else:
                send_mails(survey)
        else:
            print('No data!', file = sys.stderr)
    except Exception as error:
        print(f'Unexpected error: {str(error)}', file = sys.stderr)
