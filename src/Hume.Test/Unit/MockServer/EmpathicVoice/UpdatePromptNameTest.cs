using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class UpdatePromptNameTest : BaseMockServerTest
{
    [Test]
    public void MockServerTest()
    {
        const string requestJson = """
            {
              "name": "Updated Weather Assistant Prompt Name"
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/prompts/af699d45-2985-42cc-91b9-af9e5da3bac5")
                    .WithHeader("Content-Type", "application/json")
                    .UsingPatch()
                    .WithBodyAsJson(requestJson)
            )
            .RespondWith(WireMock.ResponseBuilders.Response.Create().WithStatusCode(200));

        Assert.DoesNotThrowAsync(async () =>
            await Client.EmpathicVoice.Prompts.UpdatePromptNameAsync(
                "af699d45-2985-42cc-91b9-af9e5da3bac5",
                new PostedPromptName { Name = "Updated Weather Assistant Prompt Name" }
            )
        );
    }
}
