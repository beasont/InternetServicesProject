namespace ComputerStore.Application.DTOs;

public record CreateProductDto(string Name, string? Description, decimal Price, int Quantity, int CategoryId);
