using Microsoft.Extensions.DependencyInjection;
using TestProject.Application.Contracts;
using TestProject.Application.Services;

namespace TestProject.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICsvService, CsvService>();
            return services;
        }
    }
}
