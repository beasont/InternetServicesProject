namespace ComputerStore.IntegrationTests
{
    using System.Linq;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using ComputerStore.Domain;
    using ComputerStore.Infrastructure;

    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var optsDescriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (optsDescriptor != null) services.Remove(optsDescriptor);

                var ctxDescriptor = services
                    .SingleOrDefault(d => d.ServiceType == typeof(AppDbContext));
                if (ctxDescriptor != null) services.Remove(ctxDescriptor);

                var inMemoryProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<AppDbContext>(options => options
                    .UseInMemoryDatabase("TestDb")
                    .UseInternalServiceProvider(inMemoryProvider));

                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.Categories.Add(new Category
                {
                    Name = "Default",
                    Description = "Default category"
                });
                db.Products.Add(new Product
                {
                    Name = "Test Product",
                    Price = 100m,
                    Quantity = 10,
                    CategoryId = 1
                });
                db.SaveChanges();
            });
        }
    }
}
