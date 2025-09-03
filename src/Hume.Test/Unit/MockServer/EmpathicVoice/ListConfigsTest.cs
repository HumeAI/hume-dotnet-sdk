using global::System.Threading.Tasks;
using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class ListConfigsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "page_number": 0,
              "page_size": 1,
              "total_pages": 1,
              "configs_page": [
                {
                  "id": "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
                  "version": 0,
                  "version_description": "",
                  "name": "Weather Assistant Config",
                  "created_on": 1715267200693,
                  "modified_on": 1715267200693,
                  "evi_version": "3",
                  "prompt": {
                    "id": "af699d45-2985-42cc-91b9-af9e5da3bac5",
                    "version": 0,
                    "version_type": "FIXED",
                    "version_description": "",
                    "name": "Weather Assistant Prompt",
                    "created_on": 1715267200693,
                    "modified_on": 1715267200693,
                    "text": "<role>You are an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.</role>"
                  },
                  "voice": {
                    "provider": "HUME_AI",
                    "name": "Ava Song",
                    "id": "5bb7de05-c8fe-426a-8fcc-ba4fc4ce9f9c"
                  },
                  "language_model": {
                    "model_provider": "ANTHROPIC",
                    "model_resource": "claude-3-7-sonnet-latest",
                    "temperature": 1
                  },
                  "ellm_model": {
                    "allow_short_responses": false
                  },
                  "tools": [],
                  "builtin_tools": [],
                  "event_messages": {
                    "on_new_chat": {
                      "enabled": false,
                      "text": ""
                    },
                    "on_inactivity_timeout": {
                      "enabled": false,
                      "text": ""
                    },
                    "on_max_duration_timeout": {
                      "enabled": false,
                      "text": ""
                    }
                  },
                  "timeouts": {
                    "inactivity": {
                      "enabled": true,
                      "duration_secs": 600
                    },
                    "max_duration": {
                      "enabled": true,
                      "duration_secs": 1800
                    }
                  }
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/configs")
                    .WithParam("page_number", "0")
                    .WithParam("page_size", "1")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var pager = await Client.EmpathicVoice.Configs.ListConfigsAsync(
            new ConfigsListConfigsRequest { PageNumber = 0, PageSize = 1 }
        );
        await foreach (var item in pager)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
