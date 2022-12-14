namespace Application.DogInfo.Queries.GetDogInfoList
{
    public sealed class DogInfoLookupDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Residence { get; set; }
        public string Breed { get; set; }
        public bool IsDisabled { get; set; }
        public double ApproximateWeight { get; set; }
        public int ImageId { get; set; }

    }
}
