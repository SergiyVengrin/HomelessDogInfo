using MediatR;

namespace Application.Image.Commands.DeleteImage
{
    public sealed class DeleteImageCommand:IRequest
    {
        public int Id { get; set; }
    }
}
