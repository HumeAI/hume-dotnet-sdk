using global::System.Threading.Tasks;
using HumeApi.Core;
using HumeApi.ExpressionMeasurement.Batch;
using HumeApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.ExpressionMeasurement.Batch;

[TestFixture]
public class GetJobPredictionsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            [
              {
                "source": {
                  "type": "url",
                  "url": "https://hume-tutorials.s3.amazonaws.com/faces.zip"
                },
                "results": {
                  "predictions": [
                    {
                      "file": "faces/100.jpg",
                      "models": {
                        "face": {
                          "metadata": null,
                          "grouped_predictions": [
                            {
                              "id": "unknown",
                              "predictions": [
                                {
                                  "frame": 0,
                                  "time": 0,
                                  "prob": 0.9994111061096191,
                                  "box": {
                                    "x": 1187.885986328125,
                                    "y": 1397.697509765625,
                                    "w": 1401.668701171875,
                                    "h": 1961.424560546875
                                  },
                                  "emotions": [
                                    {
                                      "name": "Admiration",
                                      "score": 0.10722749680280685
                                    },
                                    {
                                      "name": "Adoration",
                                      "score": 0.06395940482616425
                                    },
                                    {
                                      "name": "Aesthetic Appreciation",
                                      "score": 0.05811462551355362
                                    },
                                    {
                                      "name": "Amusement",
                                      "score": 0.14187128841876984
                                    },
                                    {
                                      "name": "Anger",
                                      "score": 0.02804684266448021
                                    },
                                    {
                                      "name": "Anxiety",
                                      "score": 0.2713485360145569
                                    },
                                    {
                                      "name": "Awe",
                                      "score": 0.33812594413757324
                                    },
                                    {
                                      "name": "Awkwardness",
                                      "score": 0.1745193600654602
                                    },
                                    {
                                      "name": "Boredom",
                                      "score": 0.23600080609321594
                                    },
                                    {
                                      "name": "Calmness",
                                      "score": 0.18988418579101562
                                    },
                                    {
                                      "name": "Concentration",
                                      "score": 0.44288986921310425
                                    },
                                    {
                                      "name": "Confusion",
                                      "score": 0.39346569776535034
                                    },
                                    {
                                      "name": "Contemplation",
                                      "score": 0.31002455949783325
                                    },
                                    {
                                      "name": "Contempt",
                                      "score": 0.048870109021663666
                                    },
                                    {
                                      "name": "Contentment",
                                      "score": 0.0579497292637825
                                    },
                                    {
                                      "name": "Craving",
                                      "score": 0.06544201076030731
                                    },
                                    {
                                      "name": "Desire",
                                      "score": 0.05526508390903473
                                    },
                                    {
                                      "name": "Determination",
                                      "score": 0.08590991795063019
                                    },
                                    {
                                      "name": "Disappointment",
                                      "score": 0.19508258998394012
                                    },
                                    {
                                      "name": "Disgust",
                                      "score": 0.031529419124126434
                                    },
                                    {
                                      "name": "Distress",
                                      "score": 0.23210826516151428
                                    },
                                    {
                                      "name": "Doubt",
                                      "score": 0.3284550905227661
                                    },
                                    {
                                      "name": "Ecstasy",
                                      "score": 0.040716782212257385
                                    },
                                    {
                                      "name": "Embarrassment",
                                      "score": 0.1467227339744568
                                    },
                                    {
                                      "name": "Empathic Pain",
                                      "score": 0.07633581757545471
                                    },
                                    {
                                      "name": "Entrancement",
                                      "score": 0.16245244443416595
                                    },
                                    {
                                      "name": "Envy",
                                      "score": 0.03267110139131546
                                    },
                                    {
                                      "name": "Excitement",
                                      "score": 0.10656816512346268
                                    },
                                    {
                                      "name": "Fear",
                                      "score": 0.3115977346897125
                                    },
                                    {
                                      "name": "Guilt",
                                      "score": 0.11615975946187973
                                    },
                                    {
                                      "name": "Horror",
                                      "score": 0.19795553386211395
                                    },
                                    {
                                      "name": "Interest",
                                      "score": 0.3136432468891144
                                    },
                                    {
                                      "name": "Joy",
                                      "score": 0.06285581737756729
                                    },
                                    {
                                      "name": "Love",
                                      "score": 0.06339752674102783
                                    },
                                    {
                                      "name": "Nostalgia",
                                      "score": 0.05866732448339462
                                    },
                                    {
                                      "name": "Pain",
                                      "score": 0.07684041559696198
                                    },
                                    {
                                      "name": "Pride",
                                      "score": 0.026822954416275024
                                    },
                                    {
                                      "name": "Realization",
                                      "score": 0.30000734329223633
                                    },
                                    {
                                      "name": "Relief",
                                      "score": 0.04414166510105133
                                    },
                                    {
                                      "name": "Romance",
                                      "score": 0.042728863656520844
                                    },
                                    {
                                      "name": "Sadness",
                                      "score": 0.14773206412792206
                                    },
                                    {
                                      "name": "Satisfaction",
                                      "score": 0.05902980640530586
                                    },
                                    {
                                      "name": "Shame",
                                      "score": 0.08103451132774353
                                    },
                                    {
                                      "name": "Surprise (negative)",
                                      "score": 0.25518184900283813
                                    },
                                    {
                                      "name": "Surprise (positive)",
                                      "score": 0.28845661878585815
                                    },
                                    {
                                      "name": "Sympathy",
                                      "score": 0.062488824129104614
                                    },
                                    {
                                      "name": "Tiredness",
                                      "score": 0.1559651643037796
                                    },
                                    {
                                      "name": "Triumph",
                                      "score": 0.01955239288508892
                                    }
                                  ],
                                  "facs": null,
                                  "descriptions": null
                                }
                              ]
                            }
                          ]
                        }
                      }
                    }
                  ],
                  "errors": []
                }
              }
            ]
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/batch/jobs/job_id/predictions")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.ExpressionMeasurement.Batch.GetJobPredictionsAsync("job_id");
        Assert.That(
            response,
            Is.EqualTo(
                    JsonUtils.Deserialize<IEnumerable<InferenceSourcePredictResult>>(mockResponse)
                )
                .UsingDefaults()
        );
    }
}
