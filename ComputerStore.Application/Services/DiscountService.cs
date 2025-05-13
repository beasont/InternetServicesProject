using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;

namespace ComputerStore.Application.Services;

public class DiscountService : IDiscountService
{
    private readonly IProductRepository _products;

    public DiscountService(IProductRepository products)
    {
        _products = products;
    }

    public async Task<BasketDiscountResponse> CalculateDiscountAsync(IEnumerable<BasketItemDto> items)
    {
        var list = items.ToList();
        decimal total = 0m;
        decimal discount = 0m;
        var categoryCounts = new Dictionary<int, int>();

        foreach (var item in list)
        {
            var product = await _products.GetByIdAsync(item.ProductId)
                          ?? throw new ArgumentException("Product not found");
            if (product.Quantity < item.Quantity)
                throw new InvalidOperationException("Not enough stock");

            total += product.Price * item.Quantity;

            if (!categoryCounts.ContainsKey(product.CategoryId))
                categoryCounts[product.CategoryId] = 0;
            categoryCounts[product.CategoryId] += item.Quantity;

            if (categoryCounts[product.CategoryId] > 1 && item.Quantity > 0)
                discount += product.Price * 0.05m;
        }

        return new BasketDiscountResponse(total, discount, total - discount);
    }
}
