using AutoDrivingCarSimulation.Application.Interfaces;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Commands.MarkCarCannotMove
{
    public class MarkCarCannotMoveCommandHandler : IRequestHandler<MarkCarCannotMoveCommand>
    {
        private readonly ICarRepository _carRepository;

        public MarkCarCannotMoveCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task Handle(MarkCarCannotMoveCommand request, CancellationToken cancellationToken)
        {
            _carRepository.MarkCarCannotMove(request.CarName, request.CollideName, request.CollideStep);
        }
    }
}
