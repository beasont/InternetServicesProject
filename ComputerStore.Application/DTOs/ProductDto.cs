namespace ComputerStore.Application.DTOs;

public record ProductDto(int Id, string Name, string? Description, decimal Price, int Quantity, int CategoryId);
