namespace ComputerStore.Tests;

using ComputerStore.Application.DTOs;
using ComputerStore.Application.Services;
using ComputerStore.Domain;
using ComputerStore.Infrastructure;
using ComputerStore.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class DiscountServiceTests
{
    [Fact]
public async Task DiscountCalculatedCorrectly()
{
        var context = new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDiscountService")
            .Options);

        context.Products.Add(new Product { Id = 1, Name = "Test Product", Price = 100m, Quantity = 5 });
        await context.SaveChangesAsync();

        var productRepository = new ProductRepository(context);

        var discountService = new DiscountService(productRepository);

        var items = new List<BasketItemDto> { new BasketItemDto(ProductId: 1, Quantity: 2) };

        var discount = await discountService.CalculateDiscountAsync(items);

        Assert.Equal(5m, discount.Discount);



    }

}
