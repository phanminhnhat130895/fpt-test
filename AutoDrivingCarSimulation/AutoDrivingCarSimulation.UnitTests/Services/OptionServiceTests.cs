using AutoDrivingCarSimulation.Application.Services;

namespace AutoDrivingCarSimulation.UnitTests.Services
{
    [Collection("Sequential")]
    public class OptionServiceTests
    {
        [Fact]
        public void ShouldReturnOptionWhenCreateNewOption()
        {
            var service = new OptionService();

            var input = new StringReader("1\n");
            Console.SetIn(input);

            var output = new StringWriter();
            Console.SetOut(output);

            var option = service.CreateOption();

            Assert.Equal(1, option);

            var consoleOutput = output.ToString();
            Assert.Contains("Please choose from the following options:", consoleOutput);
            Assert.Contains("[1] Add a car to field", consoleOutput);
            Assert.Contains("[2] Run simulation", consoleOutput);
        }
    }
}
