using FluentValidation;

namespace TestProject.Application.Models
{
    public class CreateLocationDtoValidator : AbstractValidator<CreateLocationDto>
    {
        public CreateLocationDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.OpenTime).NotEmpty().LessThan(x => x.CloseTime)
                .WithMessage("{PropertyName} must be before {ComparisonValue}");
            RuleFor(x => x.CloseTime).NotEmpty();
            RuleFor(x => x.CloseTime).GreaterThan(x => x.OpenTime)
                .WithMessage("{PropertyName} must be after {ComparisonValue}");
        }
    }
}
