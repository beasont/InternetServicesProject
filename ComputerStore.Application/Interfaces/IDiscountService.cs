using ComputerStore.Application.DTOs;

namespace ComputerStore.Application.Interfaces;

public interface IDiscountService
{
    Task<BasketDiscountResponse> CalculateDiscountAsync(IEnumerable<BasketItemDto> items);
}
