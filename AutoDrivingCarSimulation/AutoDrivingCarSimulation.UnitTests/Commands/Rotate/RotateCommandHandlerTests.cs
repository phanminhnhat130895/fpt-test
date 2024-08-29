using AutoDrivingCarSimulation.Application.Commands.Rotate;
using AutoDrivingCarSimulation.Application.Interfaces;
using AutoDrivingCarSimulation.Core.Common.Enums;
using AutoDrivingCarSimulation.Core.Entities;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Commands.Rotate
{
    public class RotateCommandHandlerTests
    {
        private readonly Mock<ICarRepository> _mockCarRepository;

        public RotateCommandHandlerTests()
        {
            _mockCarRepository = new Mock<ICarRepository>();
        }

        [Fact]
        public async Task Handle_ShouldCallRotateLeft_WhenDirectionIsL()
        {
            var car = new Mock<Car>("A", 1, 1, Direction.N, "FFFRLLFF");
            var field = new Field(10, 10);

            _mockCarRepository.Setup(x => x.GetCarByName(It.IsAny<string>())).Returns(car.Object);

            var handler = new RotateCommandHandler(_mockCarRepository.Object);

            await handler.Handle(new RotateCommand("A", 'L', field), default);

            car.Verify(c => c.RotateLeft(field), Times.Once);
            car.Verify(c => c.RotateRight(It.IsAny<Field>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldCallRotateRight_WhenDirectionIsR()
        {
            var car = new Mock<Car>("A", 1, 1, Direction.N, "FFFRLLFF");
            var field = new Field(10, 10);

            _mockCarRepository.Setup(x => x.GetCarByName(It.IsAny<string>())).Returns(car.Object);

            var handler = new RotateCommandHandler(_mockCarRepository.Object);

            await handler.Handle(new RotateCommand("A", 'R', field), default);

            car.Verify(c => c.RotateLeft(It.IsAny<Field>()), Times.Never);
            car.Verify(c => c.RotateRight(field), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldNotCallRotate_WhenCarIsNull()
        {
            var car = new Mock<Car>();
            var field = new Field(10, 10);

            _mockCarRepository.Setup(x => x.GetCarByName(It.IsAny<string>())).Returns((Car)null);

            var handler = new RotateCommandHandler(_mockCarRepository.Object);

            await handler.Handle(new RotateCommand("A", 'R', field), default);

            car.Verify(c => c.RotateLeft(It.IsAny<Field>()), Times.Never);
            car.Verify(c => c.RotateRight(It.IsAny<Field>()), Times.Never);
        }
    }
}
