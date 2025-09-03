using global::System.Threading.Tasks;
using Hume.Core;
using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class GetAudioTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "id": "369846cf-6ad5-404d-905e-a8acb5cdfc78",
              "user_id": "e6235940-cfda-3988-9147-ff531627cf42",
              "num_chats": 1,
              "page_number": 0,
              "page_size": 10,
              "total_pages": 1,
              "pagination_direction": "ASC",
              "audio_reconstructions_page": [
                {
                  "id": "470a49f6-1dec-4afe-8b61-035d3b2d63b0",
                  "user_id": "e6235940-cfda-3988-9147-ff531627cf42",
                  "status": "COMPLETE",
                  "filename": "e6235940-cfda-3988-9147-ff531627cf42/470a49f6-1dec-4afe-8b61-035d3b2d63b0/reconstructed_audio.mp4",
                  "modified_at": 1729875432555,
                  "signed_audio_url": "https://storage.googleapis.com/...etc.",
                  "signed_url_expiration_timestamp_millis": 1730232816964
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/chat_groups/369846cf-6ad5-404d-905e-a8acb5cdfc78/audio")
                    .WithParam("page_number", "0")
                    .WithParam("page_size", "10")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.ChatGroups.GetAudioAsync(
            "369846cf-6ad5-404d-905e-a8acb5cdfc78",
            new ChatGroupsGetAudioRequest
            {
                PageNumber = 0,
                PageSize = 10,
                AscendingOrder = true,
            }
        );
        Assert.That(
            response,
            Is.EqualTo(
                    JsonUtils.Deserialize<ReturnChatGroupPagedAudioReconstructions>(mockResponse)
                )
                .UsingDefaults()
        );
    }
}
