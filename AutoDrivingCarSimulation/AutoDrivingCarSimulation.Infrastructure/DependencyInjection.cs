using AutoDrivingCarSimulation.Application.Interfaces;
using AutoDrivingCarSimulation.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AutoDrivingCarSimulation.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register services
            services.AddScoped<IDataContext, DataContext>();
            services.AddTransient<ICarRepository, CarRepository>();

            return services;
        }
    }
}
