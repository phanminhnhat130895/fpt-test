using AutoDrivingCarSimulation.Application.Interfaces;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Commands.MoveForward
{
    public class MoveForwardCommandHandler : IRequestHandler<MoveForwardCommand>
    {
        private readonly ICarRepository _carRepository;

        public MoveForwardCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task Handle(MoveForwardCommand request, CancellationToken cancellationToken)
        {
            var car = _carRepository.GetCarByName(request.CarName);
            if (car != null)
            {
                car.MoveForward(request.Field);
            }
        }
    }
}
