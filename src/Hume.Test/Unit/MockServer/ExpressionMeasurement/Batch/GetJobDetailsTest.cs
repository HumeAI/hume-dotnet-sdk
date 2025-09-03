using global::System.Threading.Tasks;
using Hume.Core;
using Hume.ExpressionMeasurement.Batch;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.ExpressionMeasurement.Batch;

[TestFixture]
public class GetJobDetailsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "type": "INFERENCE",
              "job_id": "job_id",
              "request": {
                "callback_url": null,
                "files": [],
                "models": {
                  "burst": {},
                  "face": {
                    "descriptions": null,
                    "facs": null,
                    "fps_pred": 3,
                    "identify_faces": false,
                    "min_face_size": 60,
                    "prob_threshold": 0.99,
                    "save_faces": false
                  },
                  "facemesh": {},
                  "language": {
                    "granularity": "word",
                    "identify_speakers": false,
                    "sentiment": null,
                    "toxicity": null
                  },
                  "ner": {
                    "identify_speakers": false
                  },
                  "prosody": {
                    "granularity": "utterance",
                    "identify_speakers": false,
                    "window": null
                  }
                },
                "notify": true,
                "text": [],
                "urls": [
                  "https://hume-tutorials.s3.amazonaws.com/faces.zip"
                ]
              },
              "state": {
                "created_timestamp_ms": 1712590457884,
                "ended_timestamp_ms": 1712590462252,
                "num_errors": 0,
                "num_predictions": 10,
                "started_timestamp_ms": 1712590457995,
                "status": "COMPLETED"
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/batch/jobs/job_id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ExpressionMeasurement.Batch.GetJobDetailsAsync("job_id");
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<InferenceJob>(mockResponse)).UsingDefaults()
        );
    }
}
