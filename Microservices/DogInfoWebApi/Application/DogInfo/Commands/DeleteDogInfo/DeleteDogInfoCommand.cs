using MediatR;

namespace Application.DogInfo.Commands.DeleteDogInfo
{
    public sealed class DeleteDogInfoCommand:IRequest
    {
        public int Id { get; set; }
    }
}
