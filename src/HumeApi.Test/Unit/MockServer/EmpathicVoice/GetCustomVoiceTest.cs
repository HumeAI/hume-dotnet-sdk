using global::System.Threading.Tasks;
using HumeApi.Core;
using HumeApi.EmpathicVoice;
using HumeApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class GetCustomVoiceTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "id": "id",
              "version": 1,
              "name": "name",
              "created_on": 1000000,
              "modified_on": 1000000,
              "base_voice": "ITO",
              "parameter_model": "20241004-11parameter",
              "parameters": {
                "gender": 1,
                "assertiveness": 1,
                "buoyancy": 1,
                "confidence": 1,
                "enthusiasm": 1,
                "nasality": 1,
                "relaxedness": 1,
                "smoothness": 1,
                "tepidity": 1,
                "tightness": 1
              }
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/custom_voices/id")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.CustomVoices.GetCustomVoiceAsync("id");
        Assert.That(
            response,
            Is.EqualTo(JsonUtils.Deserialize<ReturnCustomVoice>(mockResponse)).UsingDefaults()
        );
    }
}
