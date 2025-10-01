using Hume.Core;
using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class CreatePromptVersionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "text": "<role>You are an updated version of an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.</role>",
              "version_description": "This is an updated version of the Weather Assistant Prompt."
            }
            """;

        const string mockResponse = """
            {
              "id": "af699d45-2985-42cc-91b9-af9e5da3bac5",
              "version": 1,
              "version_type": "FIXED",
              "version_description": "This is an updated version of the Weather Assistant Prompt.",
              "name": "Weather Assistant Prompt",
              "created_on": 1722633247488,
              "modified_on": 1722635140150,
              "text": "<role>You are an updated version of an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.</role>"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/prompts/af699d45-2985-42cc-91b9-af9e5da3bac5")
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

        var response = await Client.EmpathicVoice.Prompts.CreatePromptVersionAsync(
            "af699d45-2985-42cc-91b9-af9e5da3bac5",
            new PostedPromptVersion
            {
                Text =
                    "<role>You are an updated version of an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.</role>",
                VersionDescription = "This is an updated version of the Weather Assistant Prompt.",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ReturnPrompt?>(mockResponse)).UsingDefaults()
        );
    }
}
