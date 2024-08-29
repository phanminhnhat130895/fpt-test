using AutoDrivingCarSimulation.Application.Interfaces;
using MediatR;

namespace AutoDrivingCarSimulation.Application.Queries.GetCarByName
{
    public class GetCarByNameQueryHandler : IRequestHandler<GetCarByNameQuery, GetCarByNameQueryResponse>
    {
        private readonly ICarRepository _carRepository;

        public GetCarByNameQueryHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<GetCarByNameQueryResponse> Handle(GetCarByNameQuery request, CancellationToken cancellationToken)
        {
            var car = _carRepository.GetCarByName(request.CarName);

            return new GetCarByNameQueryResponse()
            {
                Car = car
            };
        }
    }
}
