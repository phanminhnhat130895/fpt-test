using AutoDrivingCarSimulation.Application.Interfaces;
using AutoDrivingCarSimulation.Application.Queries.GetCarByName;
using AutoDrivingCarSimulation.Domain.Common.Enums;
using AutoDrivingCarSimulation.Domain.Entities;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Queries.GetCarByName
{
    public class GetCarByNameQueryHandlerTests
    {
        private readonly Mock<ICarRepository> _mockCarRepository;

        public GetCarByNameQueryHandlerTests()
        {
            _mockCarRepository = new Mock<ICarRepository>();
        }

        [Fact]
        public async Task ShouldReturnNullWhenCarNameIsNotExist()
        {
            _mockCarRepository.Setup(x => x.GetCarByName(It.IsAny<string>())).Returns((Car)null);

            var handler = new GetCarByNameQueryHandler(_mockCarRepository.Object);

            var result = await handler.Handle(new GetCarByNameQuery("A"), default);

            Assert.NotNull(result);
            Assert.Null(result.Car);
        }

        [Fact]
        public async Task ShouldReturnDataWhenCarNameIsExisting()
        {
            _mockCarRepository.Setup(x => x.GetCarByName(It.IsAny<string>())).Returns(new Car("B", 1, 1, Direction.N, "FFFLRRFFF"));

            var handler = new GetCarByNameQueryHandler(_mockCarRepository.Object);

            var result = await handler.Handle(new GetCarByNameQuery("B"), default);

            Assert.NotNull(result);
            Assert.NotNull(result.Car);
            Assert.Equal("B", result.Car.Name);
        }
    }
}
