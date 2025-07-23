using HumeApi.Test.Unit.MockServer;
using HumeApi.Tts;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.Tts;

[TestFixture]
public class DeleteTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/tts/voices")
                    .WithParam("name", "David Hume")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.Tts.Voices.DeleteAsync(new VoicesDeleteRequest { Name = "David Hume" })
        );
    }
}
