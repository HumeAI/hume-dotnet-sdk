using global::System.Threading.Tasks;
using Hume.Core;
using Hume.Test.Unit.MockServer;
using Hume.Tts;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.Tts;

[TestFixture]
public class CreateTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "generation_id": "795c949a-1510-4a80-9646-7d0863b023ab",
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
            new PostedVoice
            {
                GenerationId = "795c949a-1510-4a80-9646-7d0863b023ab",
                Name = "David Hume",
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ReturnVoice>(mockResponse)).UsingDefaults()
        );
    }
}
