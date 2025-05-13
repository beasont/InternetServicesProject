using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IDiscountService _discount;

    public BasketController(IDiscountService discount) => _discount = discount;

    [HttpPost("discount")]
    public async Task<ActionResult<BasketDiscountResponse>> Discount([FromBody] List<BasketItemDto> items)
    {
        try
        {
            var result = await _discount.CalculateDiscountAsync(items);
            return Ok(result);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
