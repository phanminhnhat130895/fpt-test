using MediatR;

namespace AutoDrivingCarSimulation.Application.Services.Interfaces
{
    public interface ISimulationService
    {
        Task Process();
    }
}
