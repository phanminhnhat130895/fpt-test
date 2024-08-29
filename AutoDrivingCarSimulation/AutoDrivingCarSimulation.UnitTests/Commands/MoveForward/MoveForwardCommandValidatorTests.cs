using AutoDrivingCarSimulation.Application.Commands.MarkCarCannotMove;
using AutoDrivingCarSimulation.Application.Commands.MoveForward;
using AutoDrivingCarSimulation.Core.Entities;
using FluentValidation.TestHelper;

namespace AutoDrivingCarSimulation.UnitTests.Commands.MoveForward
{
    public class MoveForwardCommandValidatorTests
    {
        private readonly MoveForwardCommandValidator _validator;

        public MoveForwardCommandValidatorTests()
        {
            _validator = new MoveForwardCommandValidator();
        }

        [Fact]
        public void ShouldHaveErrorWhenCarNameIsEmpty()
        {
            var result = _validator.TestValidate(new MoveForwardCommand("", new Field(10, 10)));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("CarName is not provided"));
        }

        [Fact]
        public void ShouldHaveErrorWhenFieldIsNull()
        {
            var result = _validator.TestValidate(new MoveForwardCommand("A", null));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("Field is not provided"));
        }
    }
}
