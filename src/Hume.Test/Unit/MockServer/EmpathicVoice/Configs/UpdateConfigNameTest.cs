using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice.Configs;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class UpdateConfigNameTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        const string requestJson = """
            {
              "name": "Updated Weather Assistant Config Name"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/configs/your-config-id")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.EmpathicVoice.Configs.UpdateConfigNameAsync(
                "your-config-id",
                new PostedConfigName { Name = "Updated Weather Assistant Config Name" }
            )
        );
    }
}
