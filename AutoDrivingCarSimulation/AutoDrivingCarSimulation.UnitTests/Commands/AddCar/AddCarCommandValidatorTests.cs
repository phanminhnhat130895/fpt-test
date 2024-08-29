using AutoDrivingCarSimulation.Application.Commands.AddCar;
using AutoDrivingCarSimulation.Domain.Common.Enums;
using AutoDrivingCarSimulation.Domain.Entities;
using FluentValidation.TestHelper;

namespace AutoDrivingCarSimulation.UnitTests.Commands.AddCar
{
    public class AddCarCommandValidatorTests
    {
        private readonly AddCarCommandValidator _validator;

        public AddCarCommandValidatorTests()
        {
            _validator = new AddCarCommandValidator();
        }

        [Fact]
        public void ShouldHaveErrorWhenCarNameIsEmpty()
        {
            var result = _validator.TestValidate(new AddCarCommand(new Car("", 0, 0, Direction.N, "FFFRLFFF")));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Name is not provided"));
        }

        [Fact]
        public void ShouldHaveErrorWhenXPositionIsInvalid()
        {
            var result = _validator.TestValidate(new AddCarCommand(new Car("A", -1, 0, Direction.N, "FFFRLFFF")));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Invalid X position"));
        }

        [Fact]
        public void ShouldHaveErrorWhenYPositionIsInvalid()
        {
            var result = _validator.TestValidate(new AddCarCommand(new Car("A", 0, -1, Direction.N, "FFFRLFFF")));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Invalid Y position"));
        }

        [Fact]
        public void ShouldHaveErrorWhenCommandIsEmpty()
        {
            var result = _validator.TestValidate(new AddCarCommand(new Car("A", 0, 0, Direction.N, "")));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Command is not provided"));
        }

        [Fact]
        public void ShouldHaveErrorWhenCommandIsInvalid()
        {
            var result = _validator.TestValidate(new AddCarCommand(new Car("A", 0, 0, Direction.N, "AANBB")));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Command is invalid"));
        }
    }
}
