using Application.DogInfo.DTOs;

namespace Application.DogInfo.VMs
{
    public sealed class DogInfoListVM
    {
        public IList<DogInfoLookupDTO> DogsInfo { get; set; } = new List<DogInfoLookupDTO>();
    }
}
