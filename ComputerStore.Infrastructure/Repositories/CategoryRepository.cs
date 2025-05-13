using ComputerStore.Application.Interfaces;
using ComputerStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace ComputerStore.Infrastructure.Repositories;

public class CategoryRepository(AppDbContext db) : ICategoryRepository
{
    private readonly AppDbContext _db = db;

    public async Task<List<Category>> GetAllAsync() => await _db.Categories.AsNoTracking().ToListAsync();
    public async Task<Category?> GetByIdAsync(int id) => await _db.Categories.FindAsync(id);
    public async Task<Category?> FindByNameAsync(string name) => await _db.Categories.FirstOrDefaultAsync(c => c.Name == name);
    public async Task<bool> ExistsByNameAsync(string name) => await _db.Categories.AnyAsync(c => c.Name == name);

    public async Task<Category> AddAsync(Category category)
    {
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();
        return category;
    }

    public async Task UpdateAsync(Category category)
    {
        _db.Categories.Update(category);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();
    }
}
