using FluentValidation;

namespace Application.DogInfo.Commands.DeleteDogInfo
{
    public sealed class DeleteDogInfoCommandValidator:AbstractValidator<DeleteDogInfoCommand>
    {
        public DeleteDogInfoCommandValidator()
        {
            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");
        }
    }
}
