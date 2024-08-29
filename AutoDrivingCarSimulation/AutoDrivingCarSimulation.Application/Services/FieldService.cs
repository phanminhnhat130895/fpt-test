using AutoDrivingCarSimulation.Application.Services.Interfaces;
using AutoDrivingCarSimulation.Core.Entities;

namespace AutoDrivingCarSimulation.Application.Services
{
    public class FieldService : IFieldService
    {
        public Field CreateField()
        {
            var x = 0;
            var y = 0;
            var inputFieldData = string.Empty;

            do
            {
                Console.WriteLine("Please enter the width and height of the simulation field in x y format:");
                inputFieldData = Console.ReadLine();
            } while (!isFieldDataValid(inputFieldData, out x, out y));

            var field = new Field(x, y);
            Console.WriteLine($"You have created a field of {field.Width} x {field.Height}. \n");
            return field;
        }

        private bool isFieldDataValid(string inputFieldData, out int x, out int y)
        {
            x = 0;
            y = 0;

            if (string.IsNullOrWhiteSpace(inputFieldData)) return false;

            var fieldData = inputFieldData.Split(' ');

            if (fieldData.Length != 2) return false;

            var isValid = int.TryParse(fieldData[0], out x) && x > 0 && int.TryParse(fieldData[1], out y) && y > 0;

            return isValid;
        }
    }
}
