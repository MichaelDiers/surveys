import base64
import json

def on_survey_created(event, context):
    """Triggered from a message on a Cloud Pub/Sub topic.
    Args:
         event (dict): Event payload.
         context (google.cloud.functions.Context): Metadata for the event.
    """
    try:
        if 'data' in event:
            json_message = base64.b64decode(event['data']).decode('utf-8')
            message = json.loads(json_message)
            print(message)
        else:
            print('no data')
    except Exception as error:
        print(f'Unexpected error: {str(error)}')
