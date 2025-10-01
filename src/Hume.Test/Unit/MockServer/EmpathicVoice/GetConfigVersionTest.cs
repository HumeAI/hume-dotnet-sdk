using Hume.Core;
using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class GetConfigVersionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "id": "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
              "version": 1,
              "version_description": "",
              "name": "Weather Assistant Config",
              "created_on": 1715275452390,
              "modified_on": 1715275452390,
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
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/configs/1b60e1a0-cc59-424a-8d2c-189d354db3f3/version/1")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.Configs.GetConfigVersionAsync(
            "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
            1
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ReturnConfig>(mockResponse)).UsingDefaults()
        );
    }
}
