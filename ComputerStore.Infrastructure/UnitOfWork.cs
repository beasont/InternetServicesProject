using ComputerStore.Application.Interfaces;
using ComputerStore.Infrastructure.Repositories;

namespace ComputerStore.Infrastructure;

public class UnitOfWork(AppDbContext db) : IUnitOfWork
{
    public ICategoryRepository Categories { get; } = new CategoryRepository(db);
    public IProductRepository Products { get; } = new ProductRepository(db);
    public Task<int> SaveChangesAsync() => db.SaveChangesAsync();
}