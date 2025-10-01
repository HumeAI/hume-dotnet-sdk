using Hume.EmpathicVoice;
using Hume.Test.Unit.MockServer;
using NUnit.Framework;

namespace Hume.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class ListPromptsTest : BaseMockServerTest
{
    [NUnit.Framework.Test]
    public async System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "page_number": 0,
              "page_size": 2,
              "total_pages": 1,
              "prompts_page": [
                {
                  "id": "af699d45-2985-42cc-91b9-af9e5da3bac5",
                  "version": 0,
                  "version_type": "FIXED",
                  "version_description": "",
                  "name": "Weather Assistant Prompt",
                  "created_on": 1715267200693,
                  "modified_on": 1715267200693,
                  "text": "<role>You are an AI weather assistant providing users with accurate and up-to-date weather information. Respond to user queries concisely and clearly. Use simple language and avoid technical jargon. Provide temperature, precipitation, wind conditions, and any weather alerts. Include helpful tips if severe weather is expected.</role>"
                },
                {
                  "id": "616b2b4c-a096-4445-9c23-64058b564fc2",
                  "version": 0,
                  "version_type": "FIXED",
                  "version_description": "",
                  "name": "Web Search Assistant Prompt",
                  "created_on": 1715267200693,
                  "modified_on": 1715267200693,
                  "text": "<role>You are an AI web search assistant designed to help users find accurate and relevant information on the web. Respond to user queries promptly, using the built-in web search tool to retrieve up-to-date results. Present information clearly and concisely, summarizing key points where necessary. Use simple language and avoid technical jargon. If needed, provide helpful tips for refining search queries to obtain better results.</role>"
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/prompts")
                    .WithParam("page_number", "0")
                    .WithParam("page_size", "2")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var items = await Client.EmpathicVoice.Prompts.ListPromptsAsync(
            new PromptsListPromptsRequest { PageNumber = 0, PageSize = 2 }
        );
        await foreach (var item in items)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
