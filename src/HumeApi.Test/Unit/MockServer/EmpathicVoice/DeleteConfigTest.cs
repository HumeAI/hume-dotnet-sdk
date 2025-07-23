using HumeApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class DeleteConfigTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/configs/1b60e1a0-cc59-424a-8d2c-189d354db3f3")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.EmpathicVoice.Configs.DeleteConfigAsync(
                "1b60e1a0-cc59-424a-8d2c-189d354db3f3"
            )
        );
    }
}
