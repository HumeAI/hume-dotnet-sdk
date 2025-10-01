using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class ListChatGroupsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "page_number": 0,
              "page_size": 1,
              "total_pages": 1,
              "pagination_direction": "ASC",
              "chat_groups_page": [
                {
                  "id": "697056f0-6c7e-487d-9bd8-9c19df79f05f",
                  "first_start_timestamp": 1721844196397,
                  "most_recent_start_timestamp": 1721861821717,
                  "active": false,
                  "most_recent_chat_id": "dfdbdd4d-0ddf-418b-8fc4-80a266579d36",
                  "num_chats": 5
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/chat_groups")
                    .WithParam("page_number", "0")
                    .WithParam("page_size", "1")
                    .WithParam("config_id", "1b60e1a0-cc59-424a-8d2c-189d354db3f3")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var items = await Client.EmpathicVoice.ChatGroups.ListChatGroupsAsync(
            new ChatGroupsListChatGroupsRequest
            {
                PageNumber = 0,
                PageSize = 1,
                AscendingOrder = true,
                ConfigId = "1b60e1a0-cc59-424a-8d2c-189d354db3f3",
            }
        );
        await foreach (var item in items)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
