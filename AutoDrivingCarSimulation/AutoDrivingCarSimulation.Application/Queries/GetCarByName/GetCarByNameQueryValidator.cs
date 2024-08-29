using FluentValidation;

namespace AutoDrivingCarSimulation.Application.Queries.GetCarByName
{
    public class GetCarByNameQueryValidator : AbstractValidator<GetCarByNameQuery>
    {
        public GetCarByNameQueryValidator()
        {
            RuleFor(x => x.CarName).NotEmpty().WithMessage("CarName is not provided");
        }
    }
}
