using AutoDrivingCarSimulation.Application.Services;
using Moq;

namespace AutoDrivingCarSimulation.UnitTests.Services
{
    [Collection("Sequential")]
    public class FieldServiceTests
    {
        [Fact]
        public void ShouldReturnFieldWhenCreateNewField()
        {
            var service = new FieldService();

            var input = new StringReader("10 10\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var field = service.CreateField();

            Assert.NotNull(field);
            Assert.Equal(10, field.Width);
            Assert.Equal(10, field.Height);

            var consoleOutput = output.ToString();
            Assert.Contains("Please enter the width and height of the simulation field in x y format:", consoleOutput);
            Assert.Contains($"You have created a field of {field.Width} x {field.Height}. \n", consoleOutput);
        }
    }
}
