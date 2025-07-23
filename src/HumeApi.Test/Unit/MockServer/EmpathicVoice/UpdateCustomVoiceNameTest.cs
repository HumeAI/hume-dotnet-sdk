using global::System.Threading.Tasks;
using HumeApi.EmpathicVoice;
using HumeApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class UpdateCustomVoiceNameTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string requestJson = """
            {
              "name": "name"
            }
            """;

        const string mockResponse = "string";

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/custom_voices/id")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.CustomVoices.UpdateCustomVoiceNameAsync(
            "id",
            new PostedCustomVoiceName { Name = "name" }
        );
        Assert.That(response, Is.EqualTo(mockResponse));
    }
}
