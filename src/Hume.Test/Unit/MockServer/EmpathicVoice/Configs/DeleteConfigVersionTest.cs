using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice.Configs;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class DeleteConfigVersionTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/configs/your-config-id/version/1")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.EmpathicVoice.Configs.DeleteConfigVersionAsync("your-config-id", 1)
        );
    }
}
