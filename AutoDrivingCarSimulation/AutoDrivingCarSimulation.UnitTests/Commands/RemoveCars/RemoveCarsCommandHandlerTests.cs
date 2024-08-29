using AutoDrivingCarSimulation.Application.Commands.RemoveCars;
using AutoDrivingCarSimulation.Application.Interfaces;
using Moq;
using System.Threading;

namespace AutoDrivingCarSimulation.UnitTests.Commands.RemoveCars
{
    public class RemoveCarsCommandHandlerTests
    {
        private readonly Mock<ICarRepository> _mockCarRepository;

        public RemoveCarsCommandHandlerTests()
        {
            _mockCarRepository = new Mock<ICarRepository>();
        }

        [Fact]
        public async Task RemoveCarsSuccess()
        {
            var handler = new RemoveCarsCommandHandler(_mockCarRepository.Object);

            // Act
            await handler.Handle(new RemoveCarsCommand(), default);

            // Assert
            _mockCarRepository.Verify(r => r.RemoveCars(), Times.Once);
        }
    }
}
