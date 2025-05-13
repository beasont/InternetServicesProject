using AutoMapper;
using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;
using ComputerStore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ProductsController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var list = await _uow.Products.GetAllAsync();
        return Ok(_mapper.Map<List<ProductDto>>(list));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _uow.Products.GetByIdAsync(id);
        if (entity == null) return NotFound("Product not found");
        return Ok(_mapper.Map<ProductDto>(entity));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateProductDto dto)
    {
        if (await _uow.Products.FindByNameAsync(dto.Name) is not null)
            return Conflict("Product with that name already exists");

        if (await _uow.Categories.GetByIdAsync(dto.CategoryId) == null)
            return BadRequest("Invalid category");

        var entity = _mapper.Map<Product>(dto);
        await _uow.Products.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, _mapper.Map<ProductDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateProductDto dto)
    {
        var entity = await _uow.Products.GetByIdAsync(id);
        if (entity == null) return NotFound("Product not found");

        if (dto.Name != entity.Name && await _uow.Products.FindByNameAsync(dto.Name) is not null)
            return Conflict("Another product with that name already exists");

        _mapper.Map(dto, entity);
        await _uow.Products.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _uow.Products.GetByIdAsync(id);
        if (entity == null) return NotFound("Product not found");
        await _uow.Products.DeleteAsync(entity);
        return NoContent();
    }
}
