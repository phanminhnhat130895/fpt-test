using MediatR;

namespace AutoDrivingCarSimulation.Application.Queries.GetCarByName
{
    public class GetCarByNameQuery : IRequest<GetCarByNameQueryResponse>
    {
        public string CarName { get; }

        public GetCarByNameQuery(string carName)
        {
            CarName = carName;
        }
    }
}
