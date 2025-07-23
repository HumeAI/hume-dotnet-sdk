using global::System.Threading.Tasks;
using HumeApi.Test.Unit.MockServer;
using HumeApi.Tts;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.Tts;

[TestFixture]
public class ListTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "page_number": 0,
              "page_size": 10,
              "total_pages": 1,
              "voices_page": [
                {
                  "name": "David Hume",
                  "id": "c42352c0-4566-455d-b180-0f654b65b525",
                  "provider": "CUSTOM_VOICE"
                },
                {
                  "name": "Goliath Hume",
                  "id": "d87352b0-26a3-4b11-081b-d157a5674d19",
                  "provider": "CUSTOM_VOICE"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/tts/voices")
                    .WithParam("provider", "CUSTOM_VOICE")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var pager = await Client.Tts.Voices.ListAsync(
            new VoicesListRequest { Provider = VoiceProvider.CustomVoice }
        );
        await foreach (var item in pager)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
