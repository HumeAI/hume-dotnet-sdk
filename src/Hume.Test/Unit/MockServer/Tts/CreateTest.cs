using Hume.Test.Unit.MockServer;
using Hume.Test.Utils;
using Hume.Tts;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.Tts;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "generation_id": "",
              "name": "David Hume"
            }
            """;

        const string mockResponse = """
            {
              "id": "c42352c0-4566-455d-b180-0f654b65b525",
              "name": "David Hume",
              "provider": "CUSTOM_VOICE"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/tts/voices")
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

        var response = await Client.Tts.Voices.CreateAsync(
            new PostedVoice { GenerationId = "", Name = "David Hume" }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
