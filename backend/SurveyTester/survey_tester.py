import json
import os
import uuid

from google.cloud import pubsub_v1

publisher = pubsub_v1.PublisherClient()

def create_message():
    message_json = json.dumps({        
        "surveyId": str(uuid.uuid4()),
        "name": "A new survey",
        "participants": [
            {
                "name": os.environ.get('PARTICIPANT_NAME', 'Specified environment variable PARTICIPANT_NAME is not set.'),
                "email": os.environ.get('PARTICIPANT_EMAIL', 'Specified environment variable PARTICIPANT_EMAIL is not set.'),
            }
        ]
    })

    return message_json

def survey_tester(request):
    try:
        message = create_message()

        project_id = os.environ.get('PROJECT_ID', 'Specified environment variable PROJECT_ID is not set.')
        topic_name = os.environ.get('PUBSUB_NAME', 'Specified environment variable PUBSUB_NAME is not set.')
        topic_path = publisher.topic_path(project_id, topic_name)
        message_bytes = message.encode('utf-8')
        publish_future = publisher.publish(topic_path, data=message_bytes)
        publish_future.result()
        return "OK"
    except Exception as error:
        print(error)
        return str(error)
