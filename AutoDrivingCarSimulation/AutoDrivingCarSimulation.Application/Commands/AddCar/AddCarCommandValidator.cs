using FluentValidation;
using System.Text.RegularExpressions;

namespace AutoDrivingCarSimulation.Application.Commands.AddCar
{
    public class AddCarCommandValidator : AbstractValidator<AddCarCommand>
    {
        public AddCarCommandValidator() 
        {
            RuleFor(f1 => f1.Car)
                .ChildRules(c =>
                {
                    c.RuleFor(f2 => f2.Name).NotEmpty().WithMessage("Name is not provided");
                    c.RuleFor(f2 => f2.X).Must(v => v >= 0).WithMessage("Invalid X position");
                    c.RuleFor(f2 => f2.Y).Must(v => v >= 0).WithMessage("Invalid Y position");
                    c.RuleFor(f2 => f2.Command).NotEmpty().WithMessage("Command is not provided")
                        .Must(v =>
                        {
                            Regex sWhitespace = new Regex(@"\s+");
                            sWhitespace.Replace(v, string.Empty);

                            var validCommand = new string[3] { "F", "L", "R" };
                            var commandArr = v.Split("");

                            return !commandArr.Any(c => validCommand.Contains(c));
                        }).WithMessage("Command is invalid");
                });
        }
    }
}
