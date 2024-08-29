using MediatR;

namespace AutoDrivingCarSimulation.Application.Services.Interfaces
{
    public interface ICarService
    {
        Task CreateCar(IMediator mediator);
    }
}
