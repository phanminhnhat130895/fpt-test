using AutoDrivingCarSimulation.Application.Commands.AddCar;
using AutoDrivingCarSimulation.Application.Queries.GetCarByName;
using AutoDrivingCarSimulation.Application.Queries.GetCars;
using AutoDrivingCarSimulation.Application.Services.Interfaces;
using AutoDrivingCarSimulation.Domain.Common.Enums;
using AutoDrivingCarSimulation.Domain.Entities;
using MediatR;
using System.Text.RegularExpressions;

namespace AutoDrivingCarSimulation.Application.Services
{
    public class CarService : ICarService
    {
        public async Task CreateCar(IMediator mediator)
        {
            var name = await CreateCarName(mediator);

            var initX = 0;
            var initY = 0;
            var initDirection = CreateCarInitPosition(name, out initX, out initY);

            var command = CreateCommand(name);

            var car = new Car(name, initX, initY, initDirection, command);

            await mediator.Send(new AddCarCommand(car));

            Console.WriteLine("Your current list of cars are:");
            var cars = await mediator.Send(new GetCarsQuery());

            foreach (var c in cars.Cars)
            {
                Console.WriteLine(c.GetInfo());
            }
        }

        private async Task<string> CreateCarName(IMediator mediator)
        {
            var carName = string.Empty;

            do
            {
                Console.WriteLine("Please enter the name of the car:");
                carName = Console.ReadLine();
            } while (!(await isValidCarName(carName, mediator)));

            return carName;
        }

        private async Task<bool> isValidCarName(string carName, IMediator mediator)
        {
            if (string.IsNullOrWhiteSpace(carName)) return false;

            var car = await mediator.Send(new GetCarByNameQuery(carName));

            if (car == null || car.Car == null) return true;

            return false;
        }

        private Direction CreateCarInitPosition(string carName, out int x, out int y)
        {
            x = 0;
            y = 0;
            var direction = Direction.N;
            var inputData = string.Empty;

            do
            {
                Console.WriteLine($"Please enter initial position of car {carName} in x y Direction format:");
                inputData = Console.ReadLine();
            } while (!isPositionDataValid(inputData, out x, out y, out direction));

            return direction;
        }

        private bool isPositionDataValid(string inputData, out int x, out int y, out Direction direction)
        {
            x = 0;
            y = 0;
            direction = Direction.N;
            var validDirection = new string[4] { "N", "S", "W", "E" };

            if (string.IsNullOrWhiteSpace(inputData)) return false;

            var inputArr = inputData.Split(' ');

            if (inputArr.Length != 3) return false;

            if (!validDirection.Contains(inputArr[2])) return false;

            switch (inputArr[2])
            {
                case "N":
                    direction = Direction.N;
                    break;
                case "S":
                    direction = Direction.S;
                    break;
                case "W":
                    direction = Direction.W;
                    break;
                case "E":
                    direction = Direction.E;
                    break;
            }

            return int.TryParse(inputArr[0], out x) && int.TryParse(inputArr[1], out y);
        }

        private string CreateCommand(string carName)
        {
            var carCommand = string.Empty;

            do
            {
                Console.WriteLine($"Please enter the commands for car {carName}:");
                carCommand = Console.ReadLine();
            } while (!isValidCarCommand(carCommand));

            return carCommand;
        }

        private bool isValidCarCommand(string command)
        {
            if (string.IsNullOrWhiteSpace(command)) return false;

            Regex sWhitespace = new Regex(@"\s+");
            sWhitespace.Replace(command, string.Empty);

            var validCommand = new string[3] { "F", "L", "R" };
            var commandArr = command.Split("");

            return !commandArr.Any(c => validCommand.Contains(c));
        }
    }
}
