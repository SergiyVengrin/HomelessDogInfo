using FluentValidation;

namespace Application.Image.Commands.DeleteImage
{
    public sealed class DeleteImageCommandValidator:AbstractValidator<DeleteImageCommand>
    {
        public DeleteImageCommandValidator()
        {
            RuleFor(p => p.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("{PropertyName} should not be empty.")
                .GreaterThan(0).WithMessage("{PropertyName} should be greater than 0");
        }
    }
}
