using AutoDrivingCarSimulation.Application.Commands.AddCar;
using AutoDrivingCarSimulation.Application.Commands.Rotate;
using AutoDrivingCarSimulation.Core.Common.Enums;
using AutoDrivingCarSimulation.Core.Entities;
using FluentValidation.TestHelper;

namespace AutoDrivingCarSimulation.UnitTests.Commands.Rotate
{
    public class RotateCommandValidatorTests
    {
        private readonly RotateCommandValidator _validator;

        public RotateCommandValidatorTests()
        {
            _validator = new RotateCommandValidator();
        }

        [Fact]
        public void ShouldHaveErrorWhenCarNameIsEmpty()
        {
            var result = _validator.TestValidate(new RotateCommand("", 'L', new Field(10, 10)));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("CarName is not provided"));
        }

        [Fact]
        public void ShouldHaveErrorWhenDirectionIsEmpty()
        {
            var result = _validator.TestValidate(new RotateCommand("A", char.MinValue, new Field(10, 10)));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Direction is invalid"));
        }
    }
}
