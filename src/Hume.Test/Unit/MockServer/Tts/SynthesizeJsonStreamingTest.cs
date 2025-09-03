using Hume.Test.Unit.MockServer;
using Hume.Tts;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.Tts;

[TestFixture]
public class SynthesizeJsonStreamingTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        const string requestJson = """
            {
              "utterances": [
                {
                  "text": "Beauty is no quality in things themselves: It exists merely in the mind which contemplates them.",
                  "voice": {
                    "name": "Male English Actor",
                    "provider": "HUME_AI"
                  }
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/tts/stream/json")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPost()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Tts.SynthesizeJsonStreamingAsync(
                new PostedTts
                {
                    Utterances = new List<PostedUtterance>()
                    {
                        new PostedUtterance
                        {
                            Text =
                                "Beauty is no quality in things themselves: It exists merely in the mind which contemplates them.",
                            Voice = new PostedUtteranceVoiceWithName
                            {
                                Name = "Male English Actor",
                                Provider = VoiceProvider.HumeAi,
                            },
                        },
                    },
                }
            )
        );
    }
}
