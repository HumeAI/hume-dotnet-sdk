using HumeApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class DeleteToolVersionTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/tools/00183a3f-79ba-413d-9f3b-609864268bea/version/1")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.EmpathicVoice.Tools.DeleteToolVersionAsync(
                "00183a3f-79ba-413d-9f3b-609864268bea",
                1
            )
        );
    }
}
