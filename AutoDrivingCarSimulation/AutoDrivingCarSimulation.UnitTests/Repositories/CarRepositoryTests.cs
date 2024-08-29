using AutoDrivingCarSimulation.Domain.Common.Enums;
using AutoDrivingCarSimulation.Domain.Entities;
using AutoDrivingCarSimulation.Infrastructure;
using AutoDrivingCarSimulation.Infrastructure.Repositories;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Repositories
{
    public class CarRepositoryTests
    {
        private readonly Mock<IDataContext> _mockDataContext;
        private readonly CarRepository _carRepository;

        public CarRepositoryTests() 
        { 
            _mockDataContext = new Mock<IDataContext>();
            _mockDataContext.Setup(x => x.Cars).Returns(new List<Car>()
            {
                new Car("A", 1, 1, Direction.N, "FFLLRR"),
                new Car("B", 1, 2, Direction.W, "FFFLL"),
                new Car("C", 2, 3, Direction.E, "FFRRLFFRFF")
            });

            _carRepository = new CarRepository(_mockDataContext.Object);
        }

        [Fact]
        public void GetCarByName_ShouldReturnNull_WhenCarNameNotFound()
        {
            var carName = "Test";
            var car = _carRepository.GetCarByName(carName);
            Assert.Null(car);
        }

        [Fact]
        public void GetCarByName_ShouldReturnNull_WhenCarNameExisting()
        {
            var carName = "A";
            var car = _carRepository.GetCarByName(carName);
            Assert.NotNull(car);
            Assert.Equal(car.Name, carName);
        }

        [Fact]
        public void AddCar_ShouldSuccess()
        {
            var car = new Car("Z", 0, 0, Direction.S, "FFRRLL");
            _carRepository.AddCar(car);

            var addedCar = _carRepository.GetCarByName(car.Name);

            Assert.NotNull(addedCar);
            Assert.Equal(addedCar.Name, car.Name);
        }

        [Fact]
        public void GetAll_ShouldReturnCorrectValue()
        {
            var cars = _carRepository.GetAll();

            Assert.NotEmpty(cars);
            Assert.Equal(3, cars.Count);
        }

        [Fact]
        public void RemoveCars_ShouldSuccess()
        {
            _carRepository.RemoveCars();

            var cars = _carRepository.GetAll();

            Assert.Empty(cars);
        }

        [Fact]
        public void MarkCarCannotMove_ShouldSuccess()
        {
            _carRepository.MarkCarCannotMove("A", "B", 5);
            var car = _carRepository.GetCarByName("A");
            Assert.NotNull(car);
            Assert.Equal("A", car.Name);
            Assert.Equal("B", car.CollideName);
            Assert.Equal(5, car.CollideStep);
        }
    }
}
