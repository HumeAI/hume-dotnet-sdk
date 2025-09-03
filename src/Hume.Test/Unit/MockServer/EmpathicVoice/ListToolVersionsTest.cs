using global::System.Threading.Tasks;
using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class ListToolVersionsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "page_number": 0,
              "page_size": 10,
              "total_pages": 1,
              "tools_page": [
                {
                  "tool_type": "FUNCTION",
                  "id": "00183a3f-79ba-413d-9f3b-609864268bea",
                  "version": 1,
                  "version_type": "FIXED",
                  "version_description": "Fetches current weather and uses celsius, fahrenheit, or kelvin based on location of user.",
                  "name": "get_current_weather",
                  "created_on": 1715277014228,
                  "modified_on": 1715277602313,
                  "fallback_content": "Unable to fetch current weather.",
                  "description": "This tool is for getting the current weather.",
                  "parameters": "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\", \"kelvin\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/tools/00183a3f-79ba-413d-9f3b-609864268bea")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var pager = await Client.EmpathicVoice.Tools.ListToolVersionsAsync(
            "00183a3f-79ba-413d-9f3b-609864268bea",
            new ToolsListToolVersionsRequest()
        );
        await foreach (var item in pager)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
