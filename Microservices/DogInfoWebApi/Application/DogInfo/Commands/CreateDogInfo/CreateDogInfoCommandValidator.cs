using FluentValidation;

namespace Application.DogInfo.Commands.CreateDogInfo
{
    public sealed class CreateDogInfoCommandValidator : AbstractValidator<CreateDogInfoCommand>
    {
        public CreateDogInfoCommandValidator()
        {
            RuleFor(p => p.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Length(2, 30)
                .Must(IsValidProperName).WithMessage("{PropertyName} is not valid.");

            RuleFor(p => p.Residence)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Length(2, 30)
                .Must(IsValidProperName).WithMessage("{PropertyName} is not valid.");

            RuleFor(p => p.Breed)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .Length(2, 30)
                .Must(IsValidProperName).WithMessage("{PropertyName} is not valid.");

            RuleFor(p => p.IsDisabled)
                .Must(x => x == false || x == true);

            RuleFor(p => p.ApproximateWeight)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0")
                .NotNull();

            RuleFor(p => p.ImageId)
                .Cascade(CascadeMode.Stop)
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0")
                .NotNull();
        }


        private bool IsValidProperName(string properName)
        {
            if (!properName.All(Char.IsLetter))
            {
                return false;
            }
            if (!Char.IsUpper(properName[0]))
            {
                return false;
            }

            return true;
        }
    }
}
