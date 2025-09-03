using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class DeletePromptVersionTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/prompts/af699d45-2985-42cc-91b9-af9e5da3bac5/version/1")
                    .UsingDelete()
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.EmpathicVoice.Prompts.DeletePromptVersionAsync(
                "af699d45-2985-42cc-91b9-af9e5da3bac5",
                1
            )
        );
    }
}
