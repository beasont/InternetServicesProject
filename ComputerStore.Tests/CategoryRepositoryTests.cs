namespace ComputerStore.Tests;

using Microsoft.EntityFrameworkCore;
using Xunit;
using ComputerStore.Infrastructure;
using ComputerStore.Infrastructure.Repositories;
using ComputerStore.Domain;
using System.Threading.Tasks;

public class CategoryRepositoryTests
{
    private AppDbContext GetDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "CategoryRepoTest")
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task AddAndGetById()
    {
        var db = GetDb();
        var repo = new CategoryRepository(db);
        var cat = await repo.AddAsync(new Category { Name = "Test" });
        await db.SaveChangesAsync();
        var loaded = await repo.GetByIdAsync(cat.Id);
        Assert.Equal("Test", loaded!.Name);
    }

    [Fact]
    public async Task DeleteRemovesEntity()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb_DeleteCategory")
            .Options;

        using var context = new AppDbContext(options);
        var repository = new CategoryRepository(context);
        var category = new Category { Name = "Test" };
        await repository.AddAsync(category);
        await repository.DeleteAsync(category);

        var categories = await repository.GetAllAsync();
        Assert.DoesNotContain(categories, c => c.Name == "Test");
    }

}
