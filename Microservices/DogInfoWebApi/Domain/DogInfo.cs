namespace Domain
{
    public sealed class DogInfo : BaseEntity
    {
        public string Name { get; set; }
        public string Residence { get; set; }
        public string Breed { get; set; }
        public bool IsDisabled { get; set; }
        public double ApproximateWeight { get; set; }
        public int ImageId { get; set; }

    }
}
