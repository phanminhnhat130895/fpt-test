using FluentValidation;

namespace AutoDrivingCarSimulation.Application.Commands.MoveForward
{
    public class MoveForwardCommandValidator : AbstractValidator<MoveForwardCommand>
    {
        public MoveForwardCommandValidator() 
        { 
            RuleFor(x => x.CarName).NotEmpty().WithMessage("CarName is not provided");
            RuleFor(x => x.Field).NotNull().WithMessage("Field is not provided");
        }
    }
}
