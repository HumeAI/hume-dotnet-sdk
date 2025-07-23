using global::System.Threading.Tasks;
using HumeApi.EmpathicVoice;
using HumeApi.Test.Unit.MockServer;
using NUnit.Framework;

namespace HumeApi.Test.Unit.MockServer.EmpathicVoice;

[TestFixture]
public class ListCustomVoicesTest : BaseMockServerTest
{
    [Test]
    public async global::System.Threading.Tasks.Task MockServerTest()
    {
        const string mockResponse = """
            {
              "page_number": 1,
              "page_size": 1,
              "total_pages": 1,
              "custom_voices_page": [
                {
                  "id": "id",
                  "version": 1,
                  "name": "name",
                  "created_on": 1000000,
                  "modified_on": 1000000,
                  "base_voice": "ITO",
                  "parameter_model": "20241004-11parameter",
                  "parameters": {}
                }
              ]
            }
            """;

        Server
            .Given(
                WireMock
                    .RequestBuilders.Request.Create()
                    .WithPath("/v0/evi/custom_voices")
                    .UsingGet()
            )
            .RespondWith(
                WireMock
                    .ResponseBuilders.Response.Create()
                    .WithStatusCode(200)
                    .WithBody(mockResponse)
            );

        var pager = await Client.EmpathicVoice.CustomVoices.ListCustomVoicesAsync(
            new CustomVoicesListCustomVoicesRequest()
        );
        await foreach (var item in pager)
        {
            Assert.That(item, Is.Not.Null);
            break; // Only check the first item
        }
    }
}
