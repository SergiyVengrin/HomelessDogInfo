using MediatR;
using System.Globalization;

namespace Application.DogInfo.Commands.UpdateDogInfo
{
    public sealed class UpdateDogInfoCommand:IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Residence { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public bool IsDisabled { get; set; }
        public double ApproximateWeight { get; set; }
        public int ImageId { get; set; }
    }
}
