using AutoDrivingCarSimulation.Application.Commands.AddCar;
using AutoDrivingCarSimulation.Application.Queries.GetCars;
using AutoDrivingCarSimulation.Application.Services;
using AutoDrivingCarSimulation.Domain.Common.Enums;
using AutoDrivingCarSimulation.Domain.Entities;
using MediatR;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Services
{
    [Collection("Sequential")]
    public class CarServiceTests
    {
        private readonly Mock<IMediator> _mockMediator;

        public CarServiceTests() 
        { 
            _mockMediator = new Mock<IMediator>();

            // Mock responses for mediator.Send
            var cars = new List<Car> { new Car("TestCar", 0, 0, Direction.N, "FFFRRLFFFF") };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetCarsQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new GetCarsQueryResponse()
                        {
                            Cars = cars
                        });

            _mockMediator.Setup(m => m.Send(It.IsAny<AddCarCommand>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new AddCarCommandResponse()
                        {
                            Success = true
                        });
        }

        [Fact]
        public async Task CreateCar_ShouldCreateCarAndAddToList()
        {
            var service = new CarService();

            var input = new StringReader("A\n1 2 N\nFFFRRLFFF\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            // Act
            await service.CreateCar(_mockMediator.Object);

            // Verify AddCarCommand was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<AddCarCommand>(), It.IsAny<CancellationToken>()), Times.Once);

            // Verify GetCarsQuery was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<GetCarsQuery>(), It.IsAny<CancellationToken>()), Times.Once);

            var consoleOutput = output.ToString();
            Assert.Contains("Please enter the name of the car:", consoleOutput);
            Assert.Contains("Please enter initial position of car A in x y Direction format:", consoleOutput);
            Assert.Contains("Please enter the commands for car A:", consoleOutput);
            Assert.Contains("Your current list of cars are:", consoleOutput);
            Assert.Contains("- TestCar, (0,0) N, FFFRRLFFFF", consoleOutput);
        }
    }
}
