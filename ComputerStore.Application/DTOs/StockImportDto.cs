using System.Collections.Generic;

namespace ComputerStore.Application.DTOs;

public class StockImportDto
{
    public List<ImportProductDto> Products { get; set; } = [];
    public List<ImportCategoryDto> Categories { get; set; } = [];
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}