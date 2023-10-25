// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Azure.Messaging;
using Azure.Messaging.EventGrid;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.Sql;
using Microsoft.Extensions.Logging;

namespace Api2
{
    public class BlobStorageEventGrid
    {
        private readonly ILogger<BlobStorageEventGrid> _logger;

        public BlobStorageEventGrid(ILogger<BlobStorageEventGrid> logger)
        {
            _logger = logger;
        }

        [Function(nameof(BlobStorageEventGrid))]
        [SqlOutput("[dbo].[WorkItem]", connectionStringSetting: "SqlConnectionString")]
        public object Run([EventGridTrigger] EventGridEvent eventGridEvent)
        {
            _logger.LogInformation($"Event type: {eventGridEvent.EventType}, Event subject: {eventGridEvent.Subject}");

            var data = JsonNode.Parse(eventGridEvent.Data);

            if ((int)data["contentLength"] > 0)
            {
                WorkItem item = new WorkItem()
                {
                    Id = Guid.NewGuid(),
                    ContentName = data["blobUrl"].ToString().Substring(data["blobUrl"].ToString().LastIndexOf('/') + 1),
                    ContentLength = (int)data["contentLength"],
                    BlobUrl = data["blobUrl"].ToString()
                };

                return item;
            }

            return null;
        }
    }
}
