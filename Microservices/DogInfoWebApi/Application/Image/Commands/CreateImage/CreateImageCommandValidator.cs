using Application.Image.POCOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Image.Commands.CreateImage
{
    public sealed class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
    {
        private readonly IOptions<CorrectImageExtensions> _config;

        public CreateImageCommandValidator(IOptions<CorrectImageExtensions> config)
        {
            _config = config;


            RuleFor(p => p.Image)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .Must(IsCorrectImageExtention).WithMessage("Incorrect file extention, must be an image.");
        }


        private bool IsCorrectImageExtention(IFormFile image)
        {
            if(_config.Value.Extensions is null)
            {
                return false;
            }

            return _config.Value.Extensions.Contains(Path.GetExtension(image.FileName));
        }
    }
}
