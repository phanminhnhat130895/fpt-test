using AutoDrivingCarSimulation.Application.Interfaces;
using AutoDrivingCarSimulation.Application.Queries.GetCars;
using AutoDrivingCarSimulation.Core.Common.Enums;
using AutoDrivingCarSimulation.Core.Entities;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Queries.GetCars
{
    public class GetCarsQueryHandlerTests
    {
        private readonly Mock<ICarRepository> _mockCarRepository;

        public GetCarsQueryHandlerTests()
        {
            _mockCarRepository = new Mock<ICarRepository>();
        }

        [Fact]
        public async Task ShouldReturnEmptyListWhenCarsIsNotExist()
        {
            _mockCarRepository.Setup(x => x.GetAll()).Returns(new List<Car>());

            var handler = new GetCarsQueryHandler(_mockCarRepository.Object);

            var result = await handler.Handle(new GetCarsQuery(), default);

            Assert.NotNull(result);
            Assert.Empty(result.Cars);
        }

        [Fact]
        public async Task ShouldReturnDataWhenCarsExisting()
        {
            _mockCarRepository.Setup(x => x.GetAll()).Returns(new List<Car>()
            {
                new Car("A", 1, 1, Direction.N, "FFLLRR"),
                new Car("B", 1, 2, Direction.W, "FFFLL"),
                new Car("C", 2, 3, Direction.E, "FFRRLFFRFF")
            });

            var handler = new GetCarsQueryHandler(_mockCarRepository.Object);

            var result = await handler.Handle(new GetCarsQuery(), default);

            Assert.NotNull(result);
            Assert.Equal(3, result.Cars.Count);
        }
    }
}
