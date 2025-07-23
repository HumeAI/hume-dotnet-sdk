using global::System.Threading.Tasks;
using HumeApi.Core;
using HumeApi.Test.Unit.MockServer;
using HumeApi.Tts;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.Tts;

[TestFixture]
public class SynthesizeJsonTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
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
              },
              "num_generations": 1
            }
            """;

        const string mockResponse = """
            {
              "generations": [
                {
                  "generation_id": "795c949a-1510-4a80-9646-7d0863b023ab",
                  "duration": 7.44225,
                  "file_size": 120192,
                  "encoding": {
                    "format": "mp3",
                    "sample_rate": 48000
                  },
                  "audio": "//PExAA0DDYRvkpNfhv3JI5JZ...etc.",
                  "snippets": [
                    [
                      {
                        "audio": "//PExAA0DDYRvkpNfhv3JI5JZ...etc.",
                        "generation_id": "795c949a-1510-4a80-9646-7d0863b023ab",
                        "id": "37b1b1b1-1b1b-1b1b-1b1b-1b1b1b1b1b1b",
                        "text": "Beauty is no quality in things themselves: It exists merely in the mind which contemplates them.",
                        "utterance_index": 0
                      }
                    ]
                  ]
                }
              ],
              "request_id": "66e01f90-4501-4aa0-bbaf-74f45dc15aa725906"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/tts")
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

        var response = await Client.Tts.SynthesizeJsonAsync(
            new SynthesizeJsonRequest
            {
                Body = new PostedTts
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
                    NumGenerations = 1,
                },
            }
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ReturnTts>(mockResponse)).UsingDefaults()
        );
    }
}
