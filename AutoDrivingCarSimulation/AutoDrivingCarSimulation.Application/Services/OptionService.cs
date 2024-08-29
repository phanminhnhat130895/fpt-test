using AutoDrivingCarSimulation.Application.Services.Interfaces;

namespace AutoDrivingCarSimulation.Application.Services
{
    public class OptionService : IOptionService
    {
        public int CreateOption()
        {
            var option = 0;
            do
            {
                Console.WriteLine("Please choose from the following options:");
                Console.WriteLine("[1] Add a car to field");
                Console.WriteLine("[2] Run simulation");

                var inputOptions = Console.ReadLine();
                int.TryParse(inputOptions, out option);
            } while (option != 1 && option != 2);

            return option;
        }
    }
}
