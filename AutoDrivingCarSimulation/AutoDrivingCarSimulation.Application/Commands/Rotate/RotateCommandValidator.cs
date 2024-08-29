using FluentValidation;

namespace AutoDrivingCarSimulation.Application.Commands.Rotate
{
    public class RotateCommandValidator : AbstractValidator<RotateCommand>
    {
        public RotateCommandValidator() 
        { 
            RuleFor(x => x.CarName).NotEmpty().WithMessage("CarName is not provided");
            RuleFor(x => x.Direction).Must(c => c == 'L' || c == 'R').WithMessage("Direction is invalid");
        }
    }
}
