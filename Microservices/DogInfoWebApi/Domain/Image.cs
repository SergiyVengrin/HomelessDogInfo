namespace Domain
{
    public sealed class Image:BaseEntity
    {
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
