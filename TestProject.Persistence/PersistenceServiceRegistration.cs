using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestProject.Application.Contracts;
using TestProject.Persistence.DatabaseContext;
using TestProject.Persistence.Repositories;
using TestProject.Persistence.Seed;

namespace TestProject.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<LocationDatabaseContext>(opt =>
            {
                opt.UseInMemoryDatabase(configuration.GetSection("InMemoryDatabaseName").Value);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ILocationRepository, LocationRepository>();

            ServiceProvider scope = services.BuildServiceProvider();
            LocationDatabaseContext context = scope.GetRequiredService<LocationDatabaseContext>();
            ICsvService csvService = scope.GetRequiredService<ICsvService>();
            Seeder.SeedLocations(context, csvService);

            return services;
        }
    }
}
