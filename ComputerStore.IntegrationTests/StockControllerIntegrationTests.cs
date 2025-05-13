namespace ComputerStore.IntegrationTests;

using ComputerStore.IntegrationTests;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Collections.Generic;
using ComputerStore.Application.DTOs;
using System.Net.Http.Json;

public class StockControllerIntegrationTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public StockControllerIntegrationTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ImportCreatesNewProductAndCategory()
    {
        var importDto = new List<StockImportDto>
    {
        new StockImportDto
        {
            Products = [new ImportProductDto { Name = "TestProd" }],
            Categories = [new ImportCategoryDto { Name = "TestCat" }],
            Price = 99.99m,
            Quantity = 10
        }
    };

        var response = await _client.PostAsJsonAsync("/api/stock/import", importDto);
        response.EnsureSuccessStatusCode();

        var products = await _client.GetStringAsync("/api/products");
        Assert.Contains("TestProd", products);
    }

}
