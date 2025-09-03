using global::System.Threading.Tasks;
using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class ListToolsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "page_number": 0,
              "page_size": 2,
              "total_pages": 1,
              "tools_page": [
                {
                  "tool_type": "FUNCTION",
                  "id": "d20827af-5d8d-4f66-b6b9-ce2e3e1ea2b2",
                  "version": 0,
                  "version_type": "FIXED",
                  "version_description": "Fetches user's current location.",
                  "name": "get_current_location",
                  "created_on": 1715267200693,
                  "modified_on": 1715267200693,
                  "fallback_content": "Unable to fetch location.",
                  "description": "Fetches user's current location.",
                  "parameters": "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }}, \"required\": [\"location\"] }"
                },
                {
                  "tool_type": "FUNCTION",
                  "id": "4442f3ea-9038-40e3-a2ce-1522b7de770f",
                  "version": 0,
                  "version_type": "FIXED",
                  "version_description": "Fetches current weather and uses celsius or fahrenheit based on location of user.",
                  "name": "get_current_weather",
                  "created_on": 1715266126705,
                  "modified_on": 1715266126705,
                  "fallback_content": "Unable to fetch location.",
                  "description": "Fetches current weather and uses celsius or fahrenheit based on location of user.",
                  "parameters": "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/tools")
                    .WithParam("page_number", "0")
                    .WithParam("page_size", "2")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var pager = await Client.EmpathicVoice.Tools.ListToolsAsync(
            new ToolsListToolsRequest { PageNumber = 0, PageSize = 2 }
        );
        await foreach (var item in pager)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
