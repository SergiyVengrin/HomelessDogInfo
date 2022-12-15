using Application.DogInfo.VMs;
using MediatR;

namespace Application.DogInfo.Queries.GetDogInfoByResidence
{
    public sealed class GetDogInfoByResidenceQuery:IRequest<DogInfoListVM>
    {
        public string Residence { get; set; }
    }
}
