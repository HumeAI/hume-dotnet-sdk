using global::System.Threading.Tasks;
using HumeApi.Core;
using HumeApi.EmpathicVoice;
using HumeApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class GetToolVersionTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "tool_type": "FUNCTION",
              "id": "00183a3f-79ba-413d-9f3b-609864268bea",
              "version": 1,
              "version_type": "FIXED",
              "version_description": "Fetches current weather and uses celsius, fahrenheit, or kelvin based on location of user.",
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
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.Tools.GetToolVersionAsync(
            "00183a3f-79ba-413d-9f3b-609864268bea",
            1
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ReturnUserDefinedTool?>(mockResponse)).UsingDefaults()
        );
    }
}
