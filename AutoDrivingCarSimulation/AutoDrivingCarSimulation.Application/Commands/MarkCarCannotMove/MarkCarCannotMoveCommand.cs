using MediatR;

namespace AutoDrivingCarSimulation.Application.Commands.MarkCarCannotMove
{
    public class MarkCarCannotMoveCommand : IRequest
    {
        public string CarName { get; }
        public string CollideName { get; }
        public int CollideStep { get; }
        public MarkCarCannotMoveCommand(string carName, string collideName, int collideStep)
        {
            CarName = carName;
            CollideName = collideName;
            CollideStep = collideStep;
        }
    }
}
