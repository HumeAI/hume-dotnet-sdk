using global::System.Threading.Tasks;
using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class ListChatsTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "page_number": 0,
              "page_size": 1,
              "total_pages": 1,
              "pagination_direction": "ASC",
              "chats_page": [
                {
                  "id": "470a49f6-1dec-4afe-8b61-035d3b2d63b0",
                  "chat_group_id": "9fc18597-3567-42d5-94d6-935bde84bf2f",
                  "status": "USER_ENDED",
                  "start_timestamp": 1716244940648,
                  "end_timestamp": 1716244958546,
                  "event_count": 3,
                  "metadata": "",
                  "config": {
                    "id": "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
                    "version": 0
                  }
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/chats")
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

        var pager = await Client.EmpathicVoice.Chats.ListChatsAsync(
            new ChatsListChatsRequest
            {
                PageNumber = 0,
                PageSize = 1,
                AscendingOrder = true,
            }
        );
        await foreach (var item in pager)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
