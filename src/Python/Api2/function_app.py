import logging
import azure.functions as func
import json

app = func.FunctionApp()

@app.function_name(name="eventGridTrigger")
@app.event_grid_trigger(arg_name="event")
@app.cosmos_db_output(arg_name="document",
                      database_name="items", 
                      container_name="events", 
                      collection_name="events", 
                      create_if_not_exists=True,
                      partition_key="/id",
                      connection="CosmosConnectionString",
                      connection_string_setting="CosmosConnectionString")
def BlobStorageEventGrid(event: func.EventGridEvent, document: func.Out[func.Document]):
    result = json.dumps({
        'id': event.id,
        'data': event.get_json(),
        'topic': event.topic,
        'subject': event.subject,
        'event_type': event.event_type,        
    })
    document.set(func.Document.from_json(result))
    logging.info('Python EventGrid trigger processed an event: %s', result)