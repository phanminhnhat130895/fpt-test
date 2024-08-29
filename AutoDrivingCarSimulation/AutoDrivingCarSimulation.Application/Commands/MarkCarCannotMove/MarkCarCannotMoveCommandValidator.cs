using FluentValidation;

namespace AutoDrivingCarSimulation.Application.Commands.MarkCarCannotMove
{
    public class MarkCarCannotMoveCommandValidator : AbstractValidator<MarkCarCannotMoveCommand>
    {
        public MarkCarCannotMoveCommandValidator()
        {
            RuleFor(x => x.CarName).NotEmpty().WithMessage("CarName is not provided");
            RuleFor(x => x.CollideName).NotEmpty().WithMessage("CollideName is not provided")
                                       .When(x => x.CollideStep >= 0);
            RuleFor(x => x.CollideStep).Must(v => v >= 0).WithMessage("CollideStep is invalid")
                                       .When(x => !string.IsNullOrWhiteSpace(x.CollideName));
        }
    }
}
