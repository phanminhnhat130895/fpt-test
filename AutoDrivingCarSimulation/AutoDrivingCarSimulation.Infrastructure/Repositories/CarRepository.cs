using AutoDrivingCarSimulation.Application.Interfaces;
using AutoDrivingCarSimulation.Core.Entities;

namespace AutoDrivingCarSimulation.Infrastructure.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly IDataContext _dataContext;
        public CarRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Car GetCarByName(string name)
        {
            var car = _dataContext.Cars.FirstOrDefault(x => x.Name == name);
            return car;
        }

        public void AddCar(Car car)
        {
            _dataContext.Cars.Add(car);
        }

        public List<Car> GetAll()
        {
            var cars = _dataContext.Cars.ToList();
            return cars;
        }

        public void RemoveCars()
        {
            _dataContext.Cars.Clear();
        }

        public void MarkCarCannotMove(string name, string collideName, int collideStep)
        {
            var car = _dataContext.Cars.FirstOrDefault(x => x.Name == name);
            if (car != null)
            {
                car.CanMove = false;
                car.CollideName = collideName;
                car.CollideStep = collideStep;
            }
        }
    }
}
