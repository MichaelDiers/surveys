import json

def on_survey_created(data, context):
    """ Triggered when a survey-document is written to for the first time.
    Args:
        data (dict): The event payload.
        context (google.cloud.functions.Context): Metadata for the event.
    """
    trigger_resource = context.resource

    print('Function triggered by change to: %s' % trigger_resource)

    print('\nNew value:')
    print(json.dumps(data["value"]))