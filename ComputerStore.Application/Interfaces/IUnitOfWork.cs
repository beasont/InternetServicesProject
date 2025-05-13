namespace ComputerStore.Application.Interfaces;

public interface IUnitOfWork
{
    ICategoryRepository Categories { get; }
    IProductRepository Products { get; }
    Task<int> SaveChangesAsync();
}
