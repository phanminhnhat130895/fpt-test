using AutoDrivingCarSimulation.Application.Common;
using AutoDrivingCarSimulation.Application.Services;
using AutoDrivingCarSimulation.Application.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AutoDrivingCarSimulation.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            // Register FluentValidation
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddTransient<IFieldService, FieldService>();
            services.AddTransient<IOptionService, OptionService>();
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<ISimulationService, SimulationService>();

            return services;
        }
    }
}
