using Hume.Test.Unit.MockServer;
using Hume.Test.Utils;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice.Chats;

[TestFixture]
[Parallelizable(ParallelScope.Self)]
public class GetAudioTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "id": "470a49f6-1dec-4afe-8b61-035d3b2d63b0",
              "user_id": "e6235940-cfda-3988-9147-ff531627cf42",
              "status": "COMPLETE",
              "filename": "e6235940-cfda-3988-9147-ff531627cf42/470a49f6-1dec-4afe-8b61-035d3b2d63b0/reconstructed_audio.mp4",
              "modified_at": 1729875432555,
              "signed_audio_url": "https://storage.googleapis.com/...etc.",
              "signed_url_expiration_timestamp_millis": 1730232816964
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/chats/your-chat-id/audio")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.Chats.GetAudioAsync("your-chat-id");
        JsonAssert.AreEqual(response, mockResponse);
    }
}
