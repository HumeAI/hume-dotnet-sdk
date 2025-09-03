using global::System.Threading.Tasks;
using Hume.Core;
using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class UpdateToolDescriptionTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "version_description": "Fetches current temperature, precipitation, wind speed, AQI, and other weather conditions. Uses Celsius, Fahrenheit, or kelvin depending on user's region."
            }
            """;

        const string mockResponse = """
            {
              "tool_type": "FUNCTION",
              "id": "00183a3f-79ba-413d-9f3b-609864268bea",
              "version": 1,
              "version_type": "FIXED",
              "version_description": "Fetches current temperature, precipitation, wind speed, AQI, and other weather conditions. Uses Celsius, Fahrenheit, or kelvin depending on user's region.",
              "name": "string",
              "created_on": 1715277014228,
              "modified_on": 1715277602313,
              "fallback_content": "Unable to fetch current weather.",
              "description": "This tool is for getting the current weather.",
              "parameters": "{ \"type\": \"object\", \"properties\": { \"location\": { \"type\": \"string\", \"description\": \"The city and state, e.g. San Francisco, CA\" }, \"format\": { \"type\": \"string\", \"enum\": [\"celsius\", \"fahrenheit\", \"kelvin\"], \"description\": \"The temperature unit to use. Infer this from the users location.\" } }, \"required\": [\"location\", \"format\"] }"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/tools/00183a3f-79ba-413d-9f3b-609864268bea/version/1")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.Tools.UpdateToolDescriptionAsync(
            "00183a3f-79ba-413d-9f3b-609864268bea",
            1,
            new PostedUserDefinedToolVersionDescription
            {
                VersionDescription =
                    "Fetches current temperature, precipitation, wind speed, AQI, and other weather conditions. Uses Celsius, Fahrenheit, or kelvin depending on user's region.",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ReturnUserDefinedTool?>(mockResponse)).UsingDefaults()
        );
    }
}
