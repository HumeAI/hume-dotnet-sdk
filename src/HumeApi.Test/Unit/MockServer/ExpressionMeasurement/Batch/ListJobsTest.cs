using global::System.Threading.Tasks;
using HumeApi.Core;
using HumeApi.ExpressionMeasurement.Batch;
using HumeApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.ExpressionMeasurement.Batch;

[TestFixture]
public class ListJobsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            [
              {
                "job_id": "job_id",
                "request": {
                  "callback_url": null,
                  "files": [
                    {
                      "filename": "filename",
                      "md5sum": "md5sum",
                      "content_type": "content_type"
                    }
                  ],
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
                  "created_timestamp_ms": 1712587158717,
                  "ended_timestamp_ms": 1712587159274,
                  "num_errors": 0,
                  "num_predictions": 10,
                  "started_timestamp_ms": 1712587158800,
                  "status": "COMPLETED"
                },
                "type": "INFERENCE"
              }
            ]
            """;

        Server
            .Given(WireMock.RequestBuilders.Request.Create().WithPath("/v0/batch/jobs").UsingGet())
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ExpressionMeasurement.Batch.ListJobsAsync(
            new BatchListJobsRequest()
        );
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<IEnumerable<InferenceJob>>(mockResponse))
                .UsingDefaults()
        );
    }
}
