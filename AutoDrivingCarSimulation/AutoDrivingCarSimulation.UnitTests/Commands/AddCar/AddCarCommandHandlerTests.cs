using AutoDrivingCarSimulation.Application.Commands.AddCar;
using AutoDrivingCarSimulation.Application.Interfaces;
using AutoDrivingCarSimulation.Domain.Common.Enums;
using AutoDrivingCarSimulation.Domain.Entities;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Commands.AddCar
{
    public class AddCarCommandHandlerTests
    {
        private readonly Mock<ICarRepository> _mockCarRepository;

        public AddCarCommandHandlerTests()
        {
            _mockCarRepository = new Mock<ICarRepository>();
        }

        [Fact]
        public async Task ShouldReturnTrueWhenAddCar()
        {
            var handler = new AddCarCommandHandler(_mockCarRepository.Object);

            var result = await handler.Handle(new AddCarCommand(new Car("A", 0, 0, Direction.N, "FFFRLLF")), default);

            Assert.NotNull(result);
            Assert.True(result.Success);
        }
    }
}
