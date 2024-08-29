using AutoDrivingCarSimulation.Application.Interfaces;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Commands.RemoveCars
{
    public class RemoveCarsCommandHandler : IRequestHandler<RemoveCarsCommand>
    {
        private readonly ICarRepository _carRepository;

        public RemoveCarsCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task Handle(RemoveCarsCommand request, CancellationToken cancellationToken)
        {
            _carRepository.RemoveCars();
        }
    }
}
