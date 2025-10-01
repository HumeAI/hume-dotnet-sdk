using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class UpdateToolNameTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public void MockServerTest()
    {
        const string requestJson = """
            {
              "name": "get_current_temperature"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/tools/00183a3f-79ba-413d-9f3b-609864268bea")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.EmpathicVoice.Tools.UpdateToolNameAsync(
                "00183a3f-79ba-413d-9f3b-609864268bea",
                new PostedUserDefinedToolName { Name = "get_current_temperature" }
            )
        );
    }
}
