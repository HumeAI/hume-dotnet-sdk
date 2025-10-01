using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
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
                    .WithPath("/v0/evi/configs/1b60e1a0-cc59-424a-8d2c-189d354db3f3")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.EmpathicVoice.Configs.UpdateConfigNameAsync(
                "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
                new PostedConfigName { Name = "Updated Weather Assistant Config Name" }
            )
        );
    }
}
