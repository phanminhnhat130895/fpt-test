using AutoDrivingCarSimulation.Application.Interfaces;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Commands.Rotate
{
    public class RotateCommandHandler : IRequestHandler<RotateCommand>
    {
        private readonly ICarRepository _carRepository;

        public RotateCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task Handle(RotateCommand request, CancellationToken cancellationToken)
        {
            var car = _carRepository.GetCarByName(request.CarName);
            if (car != null)
            {
                if (request.Direction == 'L') car.RotateLeft(request.Field);
                else if (request.Direction == 'R') car.RotateRight(request.Field);
            }
        }
    }
}
