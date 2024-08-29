using AutoDrivingCarSimulation.Application.Commands.MarkCarCannotMove;
using FluentValidation.TestHelper;

namespace AutoDrivingCarSimulation.UnitTests.Commands.MarkCarCannotMove
{
    public class MarkCarCannotMoveCommandValidatorTests
    {
        private readonly MarkCarCannotMoveCommandValidator _validator;

        public MarkCarCannotMoveCommandValidatorTests()
        {
            _validator = new MarkCarCannotMoveCommandValidator();
        }

        [Fact]
        public void ShouldHaveErrorWhenCarNameIsEmpty()
        {
            var result = _validator.TestValidate(new MarkCarCannotMoveCommand("", "B", 10));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("CarName is not provided"));
        }

        [Fact]
        public void ShouldHaveErrorWhenCollideNameIsEmpty()
        {
            var result = _validator.TestValidate(new MarkCarCannotMoveCommand("A", "", 10));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("CollideName is not provided"));
        }

        [Fact]
        public void ShouldHaveErrorWhenCollideStepIsInvalid()
        {
            var result = _validator.TestValidate(new MarkCarCannotMoveCommand("A", "B", -1));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("CollideStep is invalid"));
        }
    }
}
