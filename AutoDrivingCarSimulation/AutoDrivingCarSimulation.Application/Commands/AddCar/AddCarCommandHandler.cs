using AutoDrivingCarSimulation.Application.Interfaces;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Commands.AddCar
{
    public class AddCarCommandHandler : IRequestHandler<AddCarCommand, AddCarCommandResponse>
    {
        private readonly ICarRepository _carRepository;

        public AddCarCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<AddCarCommandResponse> Handle(AddCarCommand request, CancellationToken cancellationToken)
        {
            _carRepository.AddCar(request.Car);

            return new AddCarCommandResponse() { Success = true };
        }
    }
}
