using AutoDrivingCarSimulation.Domain.Entities;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Commands.Rotate
{
    public class RotateCommand : IRequest
    {
        public string CarName { get; }
        public char Direction { get; }
        public Field Field { get; }

        public RotateCommand(string carName, char direction, Field field)
        {
            CarName = carName;
            Direction = direction;
            Field = field;
        }
    }
}
