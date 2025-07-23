using global::System.Threading.Tasks;
using HumeApi.Core;
using HumeApi.EmpathicVoice;
using HumeApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class GetPromptVersionTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "id": "af699d45-2985-42cc-91b9-af9e5da3bac5",
              "version": 0,
              "version_type": "FIXED",
              "version_description": "",
              "name": "Weather Assistant Prompt",
              "created_on": 1722633247488,
              "modified_on": 1722633247488,
              "text": "<role>You are an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.</role>"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/prompts/af699d45-2985-42cc-91b9-af9e5da3bac5/version/0")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.Prompts.GetPromptVersionAsync(
            "af699d45-2985-42cc-91b9-af9e5da3bac5",
            0
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ReturnPrompt?>(mockResponse)).UsingDefaults()
        );
    }
}
