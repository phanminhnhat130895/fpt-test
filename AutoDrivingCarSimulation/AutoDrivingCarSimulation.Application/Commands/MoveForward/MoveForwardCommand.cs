using AutoDrivingCarSimulation.Domain.Entities;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Commands.MoveForward
{
    public class MoveForwardCommand : IRequest
    {
        public string CarName { get; }
        public Field Field { get; }

        public MoveForwardCommand(string carName, Field field)
        {
            CarName = carName;
            Field = field;
        }
    }
}
