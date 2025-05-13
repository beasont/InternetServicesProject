namespace ComputerStore.Application.DTOs;

public record UpdateProductDto(string Name, string? Description, decimal Price, int Quantity, int CategoryId);
