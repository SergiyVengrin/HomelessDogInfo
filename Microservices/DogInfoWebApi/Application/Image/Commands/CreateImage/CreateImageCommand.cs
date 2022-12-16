using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Image.Commands.CreateImage
{
    public sealed class CreateImageCommand : IRequest<int>
    {
        public IFormFile Image { get; set; }
    }
}
