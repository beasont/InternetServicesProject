using ComputerStore.Application.Interfaces;
using ComputerStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Infrastructure.Repositories;

public class ProductRepository(AppDbContext db) : IProductRepository
{
    private readonly AppDbContext _db = db;

    public async Task<List<Product>> GetAllAsync() => await _db.Products.AsNoTracking().ToListAsync();
    public async Task<Product?> GetByIdAsync(int id) => await _db.Products.FindAsync(id);
    public async Task<Product?> FindByNameAsync(string name) => await _db.Products.FirstOrDefaultAsync(p => p.Name == name);

    public async Task<Product> AddAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _db.Products.Remove(product);
        await _db.SaveChangesAsync();
    }
}
