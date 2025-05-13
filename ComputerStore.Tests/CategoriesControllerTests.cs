namespace ComputerStore.Tests;

using Moq;
using Xunit;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ComputerStore.Application.Interfaces;
using ComputerStore.Application.DTOs;
using ComputerStore.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComputerStore.Application.Mapping;
using ComputerStore.WebApi.Controllers;

public class CategoriesControllerTests
{
    private readonly Mock<IUnitOfWork> uow = new Mock<IUnitOfWork>();
    private readonly IMapper mapper;

    public CategoriesControllerTests()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AppProfile>());
        mapper = config.CreateMapper();
    }

    [Fact]
    public async Task GetReturnsOkWithData()
    {
        uow.Setup(x => x.Categories.GetAllAsync())
           .ReturnsAsync(new List<Category> { new Category { Id = 1, Name = "A" } });
        var controller = new CategoriesController(uow.Object, mapper);
        var result = await controller.Get() as OkObjectResult;
        var dtos = Assert.IsType<List<CategoryDto>>(result!.Value);
        Assert.Single(dtos);
    }
}
