using AutoDrivingCarSimulation.Application.Commands.MarkCarCannotMove;
using AutoDrivingCarSimulation.Application.Commands.MoveForward;
using AutoDrivingCarSimulation.Application.Commands.RemoveCars;
using AutoDrivingCarSimulation.Application.Commands.Rotate;
using AutoDrivingCarSimulation.Application.Queries.GetCars;
using AutoDrivingCarSimulation.Application.Services.Interfaces;
using AutoDrivingCarSimulation.Core.Common.Enums;
using AutoDrivingCarSimulation.Core.Entities;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Services
{
    public class SimulationService : ISimulationService
    {
        private readonly IMediator _mediator;
        private readonly IFieldService _fieldService;
        private readonly IOptionService _optionService;
        private readonly ICarService _carService;

        public SimulationService(IMediator mediator, IFieldService fieldService, IOptionService optionService, ICarService carService)
        {
            _mediator = mediator;
            _fieldService = fieldService;
            _optionService = optionService;
            _carService = carService;
        }

        public async Task Process()
        {
            Console.WriteLine("Welcome to Auto Driving Car Simulation!\n");

            var field = _fieldService.CreateField();

            var option = _optionService.CreateOption();

            if (option == 2) return;

            do
            {
                await _carService.CreateCar(_mediator);
                option = _optionService.CreateOption();
            } while (option == 1);

            await RunCommand(_mediator, field);

            await EndProcess(_mediator);
        }

        private async Task RunCommand(IMediator mediator, Field field)
        {
            var response = await mediator.Send(new GetCarsQuery());
            var longestCommandCar = response.Cars.OrderByDescending(x => x.Command).FirstOrDefault();

            if (longestCommandCar != null)
            {
                var longestCommand = longestCommandCar.Command;
                for (int i = 0; i < longestCommand.Length; i++)
                {
                    for (int j = 0; j < response.Cars.Count; j++)
                    {
                        var car = response.Cars[j];
                        var otherCar = response.Cars.Where(c => c.Name != car.Name).ToList();
                        if (car.Command.Length > i && await canMove(car, otherCar, mediator, field, car.Command[i], i))
                        {
                            switch (car.Command[i])
                            {
                                case 'F':
                                    await mediator.Send(new MoveForwardCommand(car.Name, field));
                                    break;
                                case 'L':
                                    await mediator.Send(new RotateCommand(car.Name, 'L', field));
                                    break;
                                case 'R':
                                    await mediator.Send(new RotateCommand(car.Name, 'R', field));
                                    break;
                            }
                        }
                    }
                }
            }
        }

        private async Task<bool> canMove(Car car, List<Car> other, IMediator mediator, Field field, char command, int step)
        {
            if (!car.CanMove) return false;

            var collideCar = other.FirstOrDefault(c => c.X == car.X && c.Y == car.Y);
            if (collideCar != null)
            {
                await mediator.Send(new MarkCarCannotMoveCommand(car.Name, collideCar.Name, step + 1));
                return false;
            }

            var Facing = car.Facing;
            if (command == 'L')
            {
                Facing = (Direction)(((int)car.Facing + 3) % 4);
                return true;
            }
            else if (command == 'R')
            {
                Facing = (Direction)(((int)car.Facing + 1) % 4);
                return true;
            }
            else
            {
                int newX = car.X, newY = car.Y;

                switch (Facing)
                {
                    case Direction.N: newY++; break;
                    case Direction.E: newX++; break;
                    case Direction.S: newY--; break;
                    case Direction.W: newX--; break;
                }

                // Ensure the car stays within the field boundaries
                if (field.IsWithinBounds(newX, newY))
                {
                    return true;
                }

                car.CanMove = false;
                await mediator.Send(new MarkCarCannotMoveCommand(car.Name, "", 0));
                return false;
            }
        }

        private async Task EndProcess(IMediator mediator)
        {
            var response = await mediator.Send(new GetCarsQuery());
            Console.WriteLine("After simulation, the result is:");
            for (int i = 0; i < response.Cars.Count; i++)
            {
                Console.WriteLine(response.Cars[i].ToString());
            }

            var option = 0;
            do
            {
                Console.WriteLine("Please choose from the following options:");
                Console.WriteLine("[1] Start over");
                Console.WriteLine("[2] Exit");

                var inputOptions = Console.ReadLine();
                int.TryParse(inputOptions, out option);
            } while (option != 1 && option != 2);

            await mediator.Send(new RemoveCarsCommand());
            if (option == 1)
            {
                await Process();
            }
            else
            {
                Console.WriteLine("Thank you for running the simulation. Goodbye!");
            }
        }
    }
}
