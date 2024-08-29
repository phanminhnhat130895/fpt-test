using AutoDrivingCarSimulation.Application.Commands.AddCar;
using AutoDrivingCarSimulation.Application.Commands.MarkCarCannotMove;
using AutoDrivingCarSimulation.Application.Commands.MoveForward;
using AutoDrivingCarSimulation.Application.Commands.Rotate;
using AutoDrivingCarSimulation.Application.Queries.GetCars;
using AutoDrivingCarSimulation.Application.Services;
using AutoDrivingCarSimulation.Application.Services.Interfaces;
using AutoDrivingCarSimulation.Core.Common.Enums;
using AutoDrivingCarSimulation.Core.Entities;
using MediatR;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Services
{
    [Collection("Sequential")]
    public class SimulationServiceTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly Mock<IFieldService> _mockFieldService;
        private readonly Mock<IOptionService> _mockOptionService;
        private readonly Mock<ICarService> _mockCarService;

        public SimulationServiceTests()
        {
            _mockMediator = new Mock<IMediator>();
            _mockFieldService = new Mock<IFieldService>();
            _mockOptionService = new Mock<IOptionService>();
            _mockCarService = new Mock<ICarService>();

            _mockFieldService.Setup(x => x.CreateField()).Returns(new Field(10, 10));
            _mockOptionService.SetupSequence(x => x.CreateOption()).Returns(1).Returns(2).Returns(1).Returns(2);
            _mockCarService.Setup(x => x.CreateCar(It.IsAny<IMediator>())).Returns(Task.CompletedTask);

            var cars = new List<Car> { new Car("TestCar", 0, 0, Direction.N, "FFFRRLFFFF") };

            _mockMediator.Setup(m => m.Send(It.IsAny<GetCarsQuery>(), It.IsAny<CancellationToken>()))
                        .ReturnsAsync(new GetCarsQueryResponse()
                        {
                            Cars = cars
                        });

            _mockMediator.Setup(m => m.Send(It.IsAny<MoveForwardCommand>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.CompletedTask);

            _mockMediator.Setup(m => m.Send(It.IsAny<RotateCommand>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.CompletedTask);
        }

        [Fact]
        public async Task Process_ShouldSuccess()
        {
            var service = new SimulationService(_mockMediator.Object, _mockFieldService.Object, _mockOptionService.Object, _mockCarService.Object);

            var input = new StringReader("2\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            await service.Process();

            // Verify MoveForwardCommand was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<MoveForwardCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            // Verify RotateCommand was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<RotateCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            // Verify MarkCarCannotMoveCommand was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<MarkCarCannotMoveCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            // Verify GetCarsQuery was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<GetCarsQuery>(), It.IsAny<CancellationToken>()), Times.Exactly(2));

            var consoleOutput = output.ToString();
            Assert.Contains("Welcome to Auto Driving Car Simulation!\n", consoleOutput);
            Assert.Contains("After simulation, the result is:", consoleOutput);
            Assert.Contains("Please choose from the following options:", consoleOutput);
            Assert.Contains("[1] Start over", consoleOutput);
            Assert.Contains("[2] Exit", consoleOutput);
            Assert.Contains("Thank you for running the simulation. Goodbye!", consoleOutput);
        }

        [Fact]
        public async Task Process_ShouldSuccess_Run2Times()
        {
            var service = new SimulationService(_mockMediator.Object, _mockFieldService.Object, _mockOptionService.Object, _mockCarService.Object);

            var input = new StringReader("1\n2\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            await service.Process();

            // Verify MoveForwardCommand was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<MoveForwardCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            // Verify RotateCommand was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<RotateCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            // Verify MarkCarCannotMoveCommand was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<MarkCarCannotMoveCommand>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce);

            // Verify GetCarsQuery was sent
            _mockMediator.Verify(m => m.Send(It.IsAny<GetCarsQuery>(), It.IsAny<CancellationToken>()), Times.Exactly(4));

            var consoleOutput = output.ToString();
            Assert.Contains("Welcome to Auto Driving Car Simulation!\n", consoleOutput);
            Assert.Contains("After simulation, the result is:", consoleOutput);
            Assert.Contains("Please choose from the following options:", consoleOutput);
            Assert.Contains("[1] Start over", consoleOutput);
            Assert.Contains("[2] Exit", consoleOutput);
            Assert.Contains("Thank you for running the simulation. Goodbye!", consoleOutput);
        }
    }
}
