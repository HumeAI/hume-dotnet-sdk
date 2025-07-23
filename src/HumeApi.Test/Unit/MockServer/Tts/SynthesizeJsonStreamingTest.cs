using HumeApi.Test.Unit.MockServer;
using HumeApi.Tts;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.Tts;

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
                  "description": "Middle-aged masculine voice with a clear, rhythmic Scots lilt, rounded vowels, and a warm, steady tone with an articulate, academic quality."
                }
              ],
              "context": {
                "utterances": [
                  {
                    "text": "How can people see beauty so differently?",
                    "description": "A curious student with a clear and respectful tone, seeking clarification on Hume's ideas with a straightforward question."
                  }
                ]
              },
              "format": {
                "type": "mp3"
              }
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
                            Description =
                                "Middle-aged masculine voice with a clear, rhythmic Scots lilt, rounded vowels, and a warm, steady tone with an articulate, academic quality.",
                        },
                    },
                    Context = new PostedContextWithUtterances
                    {
                        Utterances = new List<PostedUtterance>()
                        {
                            new PostedUtterance
                            {
                                Text = "How can people see beauty so differently?",
                                Description =
                                    "A curious student with a clear and respectful tone, seeking clarification on Hume's ideas with a straightforward question.",
                            },
                        },
                    },
                    Format = new Format(new Format.Mp3(new FormatMp3())),
                }
            )
        );
    }
}
