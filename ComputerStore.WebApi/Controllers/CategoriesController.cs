using AutoMapper;
using ComputerStore.Application.DTOs;
using ComputerStore.Application.Interfaces;
using ComputerStore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public CategoriesController(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var list = await _uow.Categories.GetAllAsync();
        return Ok(_mapper.Map<List<CategoryDto>>(list));
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var entity = await _uow.Categories.GetByIdAsync(id);
        if (entity == null) return NotFound("Category not found");
        return Ok(_mapper.Map<CategoryDto>(entity));
    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCategoryDto dto)
    {
        if (await _uow.Categories.ExistsByNameAsync(dto.Name))
            return Conflict("Category with that name already exists");

        var entity = _mapper.Map<Category>(dto);
        await _uow.Categories.AddAsync(entity);
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, _mapper.Map<CategoryDto>(entity));
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put(int id, UpdateCategoryDto dto)
    {
        var entity = await _uow.Categories.GetByIdAsync(id);
        if (entity == null) return NotFound("Category not found");

        if (dto.Name != entity.Name && await _uow.Categories.ExistsByNameAsync(dto.Name))
            return Conflict("Another category with that name already exists");

        _mapper.Map(dto, entity);
        await _uow.Categories.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _uow.Categories.GetByIdAsync(id);
        if (entity == null) return NotFound("Category not found");
        await _uow.Categories.DeleteAsync(entity);
        return NoContent();
    }
}
