import base64
import datetime
import json
import os
import sys

from google.cloud import pubsub_v1
import firebase_admin
from firebase_admin import firestore

IS_PRODUCTION = bool(os.environ.get('ENV_IS_PRODUCTION', False))

if IS_PRODUCTION:
    COLLECTION_NAME = os.environ.get('ENV_COLLECTION_NAME', 'Specified environment variable is not set: ENV_COLLECTION_NAME')
    SURVEY_VIEWER_LINK = os.environ.get('ENV_SURVEY_VIEWER_LINK', 'Specified environment variable is not set: ENV_SURVEY_VIEWER_LINK')
    PROJECT_ID = os.environ.get('ENV_PROJECT_ID', 'Specified environment variable is not set: ENV_PROJECT_ID')
    TOPIC_NAME_SEND_MAIL = os.environ.get('ENV_TOPIC_NAME_SEND_MAIL', 'Specified environment variable is not set: ENV_TOPIC_NAME_SEND_MAIL')
else:
    COLLECTION_NAME = os.environ.get('ENV_COLLECTION_NAME', 'surveys')
    SURVEY_VIEWER_LINK = os.environ.get('ENV_SURVEY_VIEWER_LINK', 'https://us-central1-surveys-services-test.cloudfunctions.net/SurveyViewer/participate/')
    PROJECT_ID = os.environ.get('ENV_PROJECT_ID', 'surveys-services-test')
    TOPIC_NAME_SEND_MAIL = os.environ.get('ENV_TOPIC_NAME_SEND_MAIL', 'SEND_MAIL')

DB_FIELD_ANSWER = 'answer'
DB_FIELD_CHOICES = 'choices'
DB_FIELD_EMAIL = 'email'
DB_FIELD_GUID = 'guid'
DB_FIELD_HAS_NO_VALUE = 'hasNoValue'
DB_FIELD_HAS_VOTED = 'hasVoted'
DB_FIELD_NAME = 'name'
DB_FIELD_PARTICIPANT_IDS = 'participantIds'
DB_FIELD_PARTICIPANTS = 'participants'
DB_FIELD_QUESTION = 'question'
DB_FIELD_QUESTIONS = 'questions'
DB_FIELD_REPLY_TO_EMAIL = 'replyToEmail'
DB_FIELD_STATUS = 'status'
DB_FIELD_TIMESTAMP = 'timestamp'
DB_FIELD_UPDATED = 'updated'
DB_FIELD_VALUE = 'value'

DB_VALUE_STATUS_CLOSED = 'CLOSED'

MESSAGE_FIELD_PARTICIPANT_ID = 'participantId'

SEND_MAIL_FIELD_EMAIL = 'email'
SEND_MAIL_FIELD_EMAIL_TYPE = 'emailType'
SEND_MAIL_FIELD_NAME = 'name'
SEND_MAIL_FIELD_RECIPIENTS = 'recipients'
SEND_MAIL_FIELD_REPLY_TO = 'replyTo'
SEND_MAIL_FIELD_RESULTS = 'results'
SEND_MAIL_FIELD_SURVEY_LINK = 'surveyLink'
SEND_MAIL_FIELD_SURVEY_NAME = 'surveyName'

SEND_MAIL_VALUE_EMAIL_TYPE = 'ThankYou'

publisher = pubsub_v1.PublisherClient()
firebase_admin.initialize_app()
database = firestore.client()
transaction = database.transaction()

def send_mail_to_participant(survey, survey_result):
    """Requests a thank you mail via Pub/Sub.
    Args:
        survey (dict): Survey data from the database.
        survey_result (dict): The incoming message from Pub/Sub.
    """
    participant = [p for p in survey[DB_FIELD_PARTICIPANTS] if p[DB_FIELD_GUID] == survey_result[MESSAGE_FIELD_PARTICIPANT_ID]][0]
    
    # build the Pub/Sub message
    message = {
        SEND_MAIL_FIELD_EMAIL_TYPE: SEND_MAIL_VALUE_EMAIL_TYPE,
        SEND_MAIL_FIELD_RECIPIENTS: [
            {
                SEND_MAIL_FIELD_NAME: participant[DB_FIELD_NAME],
                SEND_MAIL_FIELD_EMAIL: participant[DB_FIELD_EMAIL]
            }
        ],
        SEND_MAIL_FIELD_SURVEY_NAME: survey[DB_FIELD_NAME],
        SEND_MAIL_FIELD_SURVEY_LINK: f'{SURVEY_VIEWER_LINK}{participant[DB_FIELD_GUID]}',
        SEND_MAIL_FIELD_REPLY_TO: survey[DB_FIELD_REPLY_TO_EMAIL],
        SEND_MAIL_FIELD_RESULTS: []
    }

    # add the answers from Pub/Sub message survey_result
    for question in survey[DB_FIELD_QUESTIONS]:
        text = question[DB_FIELD_QUESTION]
        for choice in question[DB_FIELD_CHOICES]:
            if not bool(choice.get(DB_FIELD_HAS_NO_VALUE, False)):
                if int(choice[DB_FIELD_VALUE]) == int(survey_result[question[DB_FIELD_GUID]]):
                    message[SEND_MAIL_FIELD_RESULTS].append(f'{text} {choice[DB_FIELD_ANSWER]}')
                    break

    # add the message to Pub/Sub
    topic_path = publisher.topic_path(PROJECT_ID, TOPIC_NAME_SEND_MAIL)
    message_json = json.dumps(message)
    message_bytes = message_json.encode('utf-8')
    publish_future = publisher.publish(topic_path, data=message_bytes)
    publish_future.result()

@firestore.transactional
def update_survey(transaction, survey_id, survey_result):
    """Update the survey and set the answers of the participant
    Args:
        transaction (google.cloud.firestore_v1.transaction.Transaction): Used for updating the survey.
        survey_id (string): The document id in firestore.
        survey_result (dict): The incoming message from Pub/Sub.
    """
    # read the survey by id
    doc_ref = database.collection(COLLECTION_NAME).document(survey_id)
    doc = doc_ref.get(transaction=transaction)

    # set the answers for the participant
    participants = doc.to_dict()[DB_FIELD_PARTICIPANTS]
    participant = [p for p in participants if p[DB_FIELD_GUID] == survey_result[MESSAGE_FIELD_PARTICIPANT_ID]][0]
    for key, value in survey_result.items():
        if key != MESSAGE_FIELD_PARTICIPANT_ID:
            participant[key] = value
    participant[DB_FIELD_UPDATED] = datetime.datetime.now(datetime.timezone.utc).isoformat()
    participant[DB_FIELD_HAS_VOTED] = True

    # the update data
    update = { DB_FIELD_PARTICIPANTS: participants, DB_FIELD_TIMESTAMP: firestore.SERVER_TIMESTAMP }
    
    # set state closed if all participants voted
    if all(bool(p.get(DB_FIELD_HAS_VOTED, False)) for p in participants):
        update[DB_FIELD_STATUS] = DB_VALUE_STATUS_CLOSED

    # execute the update
    transaction.update(doc_ref, update)
    return DB_FIELD_STATUS in update

def evaluate_survey_result(survey_result):
    """Processes the Pub/Sub message.
    Args:
        survey_result (dict): The incoming message from Pub/Sub.
    """
    # read survey from database
    documents = database.collection(COLLECTION_NAME).where(DB_FIELD_PARTICIPANT_IDS, 'array_contains', survey_result[MESSAGE_FIELD_PARTICIPANT_ID]).limit(1).get()
    if len(documents) != 1:
        raise Exception(f'No data found for {MESSAGE_FIELD_PARTICIPANT_ID}: {str(survey_result[MESSAGE_FIELD_PARTICIPANT_ID])}')
    survey = documents[0].to_dict()

    # reject the update if the survey is in closed state
    if survey[DB_FIELD_STATUS] == DB_VALUE_STATUS_CLOSED:
        raise Exception(f'Survey is closed: {documents[0].id} - {survey_result}')

    # check questions and answers
    for question in survey[DB_FIELD_QUESTIONS]:
        if question[DB_FIELD_GUID] not in survey_result or not any(int(survey_result[question[DB_FIELD_GUID]]) == int(choice[DB_FIELD_VALUE]) for choice in question[DB_FIELD_CHOICES] if DB_FIELD_HAS_NO_VALUE not in choice or not bool(choice[DB_FIELD_HAS_NO_VALUE])):
            raise Exception(f'Invalid data for question {question[DB_FIELD_GUID]} - {str(survey_result)}')
    
    # update in transaction
    if update_survey(transaction, documents[0].id, survey_result):
        pass

    # send thank you mail
    send_mail_to_participant(survey, survey_result)

    return True

def on_evaluate_survey(event, context):
    """Triggered from a message on a Cloud Pub/Sub topic.
    Args:
         event (dict): Event payload.
         context (google.cloud.functions.Context): Metadata for the event.
    """
    try:
        if 'data' in event:
            json_message = base64.b64decode(event['data']).decode('utf-8')
            survey_result = json.loads(json_message)
            evaluate_survey_result(survey_result)
        else:
            print('No data!', file = sys.stderr)
    except Exception as error:
        print(f'Unexpected error: {str(error)}', file = sys.stderr)
