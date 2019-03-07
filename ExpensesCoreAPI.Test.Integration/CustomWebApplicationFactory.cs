using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ExpensesCoreAPI.Test.Integration
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                services.AddDbContext<ExpensesCoreAPI.Data.ExpensesContext>(options =>
                {
                    options.UseInMemoryDatabase("ExpensesDBTest");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedService = scope.ServiceProvider;
                    var db = scopedService.GetRequiredService<ExpensesCoreAPI.Data.ExpensesContext>();

                    db.Database.EnsureCreated();

                    SeedData.InitializeTestDB(db);
                }
            });
        }
    }
}
