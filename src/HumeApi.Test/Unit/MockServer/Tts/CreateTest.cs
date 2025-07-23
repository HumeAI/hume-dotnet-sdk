using global::System.Threading.Tasks;
using HumeApi.Core;
using HumeApi.Test.Unit.MockServer;
using HumeApi.Tts;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.Tts;

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
              "name": "David Hume",
              "id": "c42352c0-4566-455d-b180-0f654b65b525",
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
