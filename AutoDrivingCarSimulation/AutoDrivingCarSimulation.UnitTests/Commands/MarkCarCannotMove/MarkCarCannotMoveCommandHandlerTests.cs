using AutoDrivingCarSimulation.Application.Commands.MarkCarCannotMove;
using AutoDrivingCarSimulation.Application.Interfaces;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Commands.MarkCarCannotMove
{
    public class MarkCarCannotMoveCommandHandlerTests
    {
        private readonly Mock<ICarRepository> _mockCarRepository;

        public MarkCarCannotMoveCommandHandlerTests()
        {
            _mockCarRepository = new Mock<ICarRepository>();
        }

        [Fact]
        public async Task MarkCarCannotMoveSuccess()
        {
            var handler = new MarkCarCannotMoveCommandHandler(_mockCarRepository.Object);

            // Act
            await handler.Handle(new MarkCarCannotMoveCommand("A", "B", 1), default);

            // Assert
            _mockCarRepository.Verify(r => r.MarkCarCannotMove(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
        }
    }
}
