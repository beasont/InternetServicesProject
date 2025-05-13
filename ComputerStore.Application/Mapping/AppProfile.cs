using AutoMapper;
using ComputerStore.Application.DTOs;
using ComputerStore.Domain;

namespace ComputerStore.Application.Mapping;

public class AppProfile : Profile
{
    public AppProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductDto, Product>();
        CreateMap<UpdateProductDto, Product>();
    }
}
