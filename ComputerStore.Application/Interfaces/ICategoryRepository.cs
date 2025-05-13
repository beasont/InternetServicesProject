using ComputerStore.Domain;

namespace ComputerStore.Application.Interfaces;

public interface ICategoryRepository
{
    Task<List<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(int id);
    Task<Category?> FindByNameAsync(string name);
    Task<bool> ExistsByNameAsync(string name);
    Task<Category> AddAsync(Category category);
    Task UpdateAsync(Category category);
    Task DeleteAsync(Category category);
}
