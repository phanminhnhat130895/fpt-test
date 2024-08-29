using AutoDrivingCarSimulation.Domain.Entities;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Commands.AddCar
{
    public class AddCarCommand : IRequest<AddCarCommandResponse>
    {
        public Car Car { get; }

        public AddCarCommand(Car car)
        {
            Car = car;
        }
    }
}
