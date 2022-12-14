using Application.Common.Exceptions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Linq.Expressions;

namespace Application.DogInfo.Commands.UpdateDogInfo
{
    public sealed class UpdateDogInfoCommandHandler : IRequestHandler<UpdateDogInfoCommand>
    {
        private readonly IConfiguration _configuration;

        public UpdateDogInfoCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Unit> Handle(UpdateDogInfoCommand request, CancellationToken cancellationToken)
        {
            var query = "SELECT * FROM dogs_info WHERE id = @Id";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                var dogInfo = await connection.QuerySingleOrDefaultAsync<Domain.DogInfo>(query, new {Id = request.Id});

                if (dogInfo is null)
                {
                    throw new NotFoundException(request.Id);
                }
            }


            query = "UPDATE dogs_info SET name = @Name, residence = @Residence, breed = @Breed, is_disabled = @IsDisabled, approximate_weight = @ApproximateWeight, image_id = @ImageId WHERE id = @Id";

            var parameters = new DynamicParameters();
            parameters.Add("Name", request.Name);
            parameters.Add("Residence", request.Residence);
            parameters.Add("Breed", request.Breed);
            parameters.Add("IsDisabled", request.IsDisabled);
            parameters.Add("ApproximateWeight", request.ApproximateWeight);
            parameters.Add("ImageId", request.ImageId);

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync(query, parameters);
            }

            return Unit.Value;
        }
    }
}
