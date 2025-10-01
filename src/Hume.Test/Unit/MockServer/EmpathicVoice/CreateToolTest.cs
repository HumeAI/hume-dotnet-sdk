using Hume.Core;
using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class CreateToolTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "name": "get_current_weather",
              "parameters": "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }",
              "version_description": "Fetches current weather and uses celsius or fahrenheit based on location of user.",
              "description": "This tool is for getting the current weather.",
              "fallback_content": "Unable to fetch current weather."
            }
            """;

        const string mockResponse = """
            {
              "tool_type": "FUNCTION",
              "id": "aa9b71c4-723c-47ff-9f83-1a1829e74376",
              "version": 0,
              "version_type": "FIXED",
              "version_description": "Fetches current weather and uses celsius or fahrenheit based on location of user.",
              "name": "get_current_weather",
              "created_on": 1715275452390,
              "modified_on": 1715275452390,
              "fallback_content": "Unable to fetch current weather.",
              "description": "This tool is for getting the current weather.",
              "parameters": "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/tools")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.Tools.CreateToolAsync(
            new PostedUserDefinedTool
            {
                Name = "get_current_weather",
                Parameters =
                    "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }",
                VersionDescription =
                    "Fetches current weather and uses celsius or fahrenheit based on location of user.",
                Description = "This tool is for getting the current weather.",
                FallbackContent = "Unable to fetch current weather.",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ReturnUserDefinedTool?>(mockResponse)).UsingDefaults()
        );
    }
}
