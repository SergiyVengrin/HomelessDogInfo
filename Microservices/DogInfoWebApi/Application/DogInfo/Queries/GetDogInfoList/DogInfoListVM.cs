namespace Application.DogInfo.Queries.GetDogInfoList
{
    public sealed class DogInfoListVM
    {
        public IList<DogInfoLookupDTO> DogsInfo { get; set; } = new List<DogInfoLookupDTO>();
    }
}
