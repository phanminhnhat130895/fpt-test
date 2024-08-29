using AutoDrivingCarSimulation.Application.Interfaces;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Queries.GetCars
{
    public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, GetCarsQueryResponse>
    {
        private readonly ICarRepository _carRepository;

        public GetCarsQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<GetCarsQueryResponse> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            var cars = _carRepository.GetAll();
            return new GetCarsQueryResponse { Cars = cars };
        }
    }
}
