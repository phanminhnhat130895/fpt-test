using AutoDrivingCarSimulation.Application.Queries.GetCarByName;
using FluentValidation.TestHelper;

namespace AutoDrivingCarSimulation.UnitTests.Queries.GetCarByName
{
    public class GetCarByNameQueryValidatorTests
    {
        private readonly GetCarByNameQueryValidator _validator;

        public GetCarByNameQueryValidatorTests()
        {
            _validator = new GetCarByNameQueryValidator();
        }

        [Fact]
        public void ShouldHaveErrorWhenCarNameIsEmpty()
        {
            var result = _validator.TestValidate(new GetCarByNameQuery(""));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("CarName is not provided"));
        }

        [Fact]
        public void ShouldHaveErrorWhenCarNameIsNull()
        {
            var result = _validator.TestValidate(new GetCarByNameQuery(null));

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.ErrorMessage.Equals("CarName is not provided"));
        }

        [Fact]
        public void ShouldHaveNoErrorWhenCarNameIsValid()
        {
            var result = _validator.TestValidate(new GetCarByNameQuery("A"));

            Assert.True(result.IsValid);
        }
    }
}
