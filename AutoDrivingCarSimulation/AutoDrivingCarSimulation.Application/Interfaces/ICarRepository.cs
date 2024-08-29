using AutoDrivingCarSimulation.Domain.Entities;

namespace AutoDrivingCarSimulation.Application.Interfaces
{
    public interface ICarRepository
    {
        Car GetCarByName(string name);
        void AddCar(Car car);
        List<Car> GetAll();
        void RemoveCars();
        void MarkCarCannotMove(string name, string collideName, int collideStep);
    }
}
