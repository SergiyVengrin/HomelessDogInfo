using MediatR;

namespace Application.DogInfo.Queries.GetDogInfoById
{
    public sealed class GetDogInfoByIdQuery:IRequest<DogInfoVM>
    {
        public int Id { get; set; }
    }
}
