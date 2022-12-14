using MediatR;

namespace Application.DogInfo.Commands.CreateDogInfo
{
    public sealed class CreateDogInfoCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Residence { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public bool IsDisabled { get; set; }
        public double ApproximateWeight { get; set; }
        public int ImageId { get; set; }
    }
}
