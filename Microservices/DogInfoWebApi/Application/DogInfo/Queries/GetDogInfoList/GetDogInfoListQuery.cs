using Application.DogInfo.VMs;
using MediatR;

namespace Application.DogInfo.Queries.GetDogInfoList
{
    public sealed class GetDogInfoListQuery:IRequest<DogInfoListVM>
    {
    }
}
