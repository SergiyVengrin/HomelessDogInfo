using Application.Common.Exceptions;
using Application.DogInfo.DTOs;
using Application.DogInfo.VMs;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Application.DogInfo.Queries.GetDogInfoByName
{
    public sealed class GetDogInfoByNameQueryHandler : IRequestHandler<GetDogInfoByNameQuery, DogInfoListVM>
    {
        private readonly IConfiguration _configuration;

        public GetDogInfoByNameQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<DogInfoListVM> Handle(GetDogInfoByNameQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT id, name, residence, breed, is_disabled AS IsDisabled, approximate_weight AS ApproximateWeight, image_id AS ImageId FROM dogs_info " +
                "WHERE name = @Name";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                var result = await connection.QueryAsync<DogInfoLookupDTO>(query, new { Name = request.Name });

                if (!result.Any())
                {
                    throw new NotFoundException(request.Name);
                }

                return new DogInfoListVM { DogsInfo = result.ToList() };
            }
        }
    }
}
