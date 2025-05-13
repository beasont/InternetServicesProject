using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;
using ComputerStore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController(IUnitOfWork uow) : ControllerBase
{
    private readonly IUnitOfWork _uow = uow;

    [HttpPost("import")]
    public async Task<IActionResult> Import([FromBody] List<StockImportDto> imports)
    {
        foreach (var import in imports)
        {
            var catDto = import.Categories.First();
            var categoryName = catDto.Name.Trim();
            var categoryDesc = catDto.Description.Trim();
            var category = await _uow.Categories.FindByNameAsync(categoryName);
            if (category == null)
            {
                category = new Category
                {
                    Name = categoryName,
                    Description = categoryDesc
                };
                await _uow.Categories.AddAsync(category);
            }

            foreach (var prodDto in import.Products)
            {
                var prodName = prodDto.Name.Trim();
                var prodDesc = prodDto.Description.Trim();
                var product = await _uow.Products.FindByNameAsync(prodName);
                if (product == null)
                {
                    product = new Product
                    {
                        Name = prodName,
                        Description = prodDesc,
                        Price = import.Price,
                        Quantity = import.Quantity,
                        CategoryId = category.Id
                    };
                    await _uow.Products.AddAsync(product);
                }
                else
                {
                    product.Quantity += import.Quantity;
                    product.Description = prodDesc;
                    await _uow.Products.UpdateAsync(product);
                }
            }
        }

        return Ok();
    }
}
