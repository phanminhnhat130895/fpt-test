using AutoDrivingCarSimulation.Application.Commands.MoveForward;
using AutoDrivingCarSimulation.Application.Commands.Rotate;
using AutoDrivingCarSimulation.Application.Interfaces;
using AutoDrivingCarSimulation.Domain.Common.Enums;
using AutoDrivingCarSimulation.Domain.Entities;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Commands.MoveForward
{
    public class MoveForwardCommandHandlerTests
    {
        private readonly Mock<ICarRepository> _mockCarRepository;

        public MoveForwardCommandHandlerTests()
        {
            _mockCarRepository = new Mock<ICarRepository>();
        }

        [Fact]
        public async Task Handle_MoveForward_ShouldSuccess()
        {
            var car = new Mock<Car>("A", 1, 1, Direction.N, "FFFRLLFF");
            var field = new Field(10, 10);

            _mockCarRepository.Setup(x => x.GetCarByName(It.IsAny<string>())).Returns(car.Object);

            var handler = new MoveForwardCommandHandler(_mockCarRepository.Object);

            await handler.Handle(new MoveForwardCommand("A", field), default);

            car.Verify(c => c.RotateLeft(It.IsAny<Field>()), Times.Never);
            car.Verify(c => c.RotateRight(It.IsAny<Field>()), Times.Never);
            car.Verify(c => c.MoveForward(field, null), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldNotCall_WhenCarIsNull()
        {
            var car = new Mock<Car>();
            var field = new Field(10, 10);

            _mockCarRepository.Setup(x => x.GetCarByName(It.IsAny<string>())).Returns((Car)null);

            var handler = new MoveForwardCommandHandler(_mockCarRepository.Object);

            await handler.Handle(new MoveForwardCommand("A", field), default);

            car.Verify(c => c.RotateLeft(It.IsAny<Field>()), Times.Never);
            car.Verify(c => c.RotateRight(It.IsAny<Field>()), Times.Never);
            car.Verify(c => c.MoveForward(field, null), Times.Never);
        }
    }
}
