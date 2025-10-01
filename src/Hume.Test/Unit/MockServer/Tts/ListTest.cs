using Hume.Test.Unit.MockServer;
using Hume.Tts;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.Tts;

[TestFixture]
public class ListTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "page_number": 0,
              "page_size": 10,
              "total_pages": 1,
              "voices_page": [
                {
                  "id": "c42352c0-4566-455d-b180-0f654b65b525",
                  "name": "David Hume",
                  "provider": "CUSTOM_VOICE"
                },
                {
                  "id": "d87352b0-26a3-4b11-081b-d157a5674d19",
                  "name": "Goliath Hume",
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

        var items = await Client.Tts.Voices.ListAsync(
            new VoicesListRequest { Provider = Hume.Tts.VoiceProvider.CustomVoice }
        );
        await foreach (var item in items)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
