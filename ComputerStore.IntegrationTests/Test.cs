namespace ComputerStore.IntegrationTests;

using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

public class ApiSmokeTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public ApiSmokeTest(WebApplicationFactory<Program> factory) => _factory = factory;

    [Fact]
    public async Task CategoriesEndpointReturnsOk()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/categories");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
