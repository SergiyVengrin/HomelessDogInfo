using Application.DogInfo.VMs;
using MediatR;

namespace Application.DogInfo.Queries.GetDogInfoByName
{
    public sealed class GetDogInfoByNameQuery:IRequest<DogInfoListVM>
    {
        public string Name { get; set; }
    }
}
