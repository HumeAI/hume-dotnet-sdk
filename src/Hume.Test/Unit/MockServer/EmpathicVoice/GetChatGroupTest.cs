using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using Hume.Test.Utils;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class GetChatGroupTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "id": "369846cf-6ad5-404d-905e-a8acb5cdfc78",
              "first_start_timestamp": 1712334213647,
              "most_recent_start_timestamp": 1712334213647,
              "num_chats": 1,
              "page_number": 0,
              "page_size": 1,
              "total_pages": 1,
              "pagination_direction": "ASC",
              "chats_page": [
                {
                  "id": "6375d4f8-cd3e-4d6b-b13b-ace66b7c8aaa",
                  "chat_group_id": "369846cf-6ad5-404d-905e-a8acb5cdfc78",
                  "status": "USER_ENDED",
                  "start_timestamp": 1712334213647,
                  "end_timestamp": 1712334332571,
                  "event_count": 0
                }
              ],
              "active": false
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/chat_groups/your-chat-group-id")
                    .WithParam("page_number", "0")
                    .WithParam("page_size", "1")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var response = await Client.EmpathicVoice.ChatGroups.GetChatGroupAsync(
            "your-chat-group-id",
            new ChatGroupsGetChatGroupRequest
            {
                PageNumber = 0,
                PageSize = 1,
                AscendingOrder = true,
            }
        );
        JsonAssert.AreEqual(response, mockResponse);
    }
}
