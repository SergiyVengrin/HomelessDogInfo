using Dapper;
using Npgsql;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Application.DogInfo.Commands
{
    public sealed class CreateDogInfoCommand : IRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Residence { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public bool IsDisabled { get; set; }
        public double ApproximateWeight { get; set; }
        public int ImageId { get; set; }


        public sealed class CreateDogInfoCommandHandler : IRequestHandler<CreateDogInfoCommand>
        {
            private readonly IConfiguration _configuration;

            public CreateDogInfoCommandHandler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Unit> Handle(CreateDogInfoCommand request, CancellationToken cancellationToken)
            {
                var query = "INSERT INTO dogs_info (name, residence, breed, is_disabled, approximate_weight, image_id) " +
                    "VALUES (@Name, @Residence, @Breed, @IsDisabled, @ApproximateWeight, @ImageId)";

                var parameters = new DynamicParameters();
                parameters.Add("Name", request.Name);
                parameters.Add("Residence", request.Residence);
                parameters.Add("Breed", request.Breed);
                parameters.Add("IsDisabled", request.IsDisabled);
                parameters.Add("ApproximateWeight", request.ApproximateWeight);
                parameters.Add("ImageId", request.ImageId);

                using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
                {
                    await connection.OpenAsync();
                    await connection.ExecuteAsync(query, parameters);
                }

                return Unit.Value;
            }
        }
    }
}
