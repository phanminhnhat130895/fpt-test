using AutoDrivingCarSimulation.Application;
using AutoDrivingCarSimulation.Application.Services.Interfaces;
using AutoDrivingCarSimulation.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoDrivingCarSimulation
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var host = CreateHostBuilder(args).Build();

            // Resolve services
            var service = host.Services.GetRequiredService<ISimulationService>();

            await service.Process();

            Console.ReadLine();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((_, services) =>
            {
                services.AddApplication();

                services.AddInfrastructure();
            });
    }
}
