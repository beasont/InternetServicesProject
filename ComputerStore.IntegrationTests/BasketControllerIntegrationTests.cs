using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ComputerStore.Application.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ComputerStore.IntegrationTests;

public class BasketControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public BasketControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task DiscountEndpointWorksEndToEnd()
    {
        var items = new List<BasketItemDto>
        {
            new BasketItemDto(1, 2)
        };

        var response = await _client.PostAsJsonAsync("/api/basket/discount", items);
        response.EnsureSuccessStatusCode();

        var dto = await response.Content.ReadFromJsonAsync<BasketDiscountResponse>();
        Assert.NotNull(dto);

        Assert.Equal(200m, dto.Total);
        Assert.Equal(5m, dto.Discount);
        Assert.Equal(195m, dto.FinalTotal);
    }
}
