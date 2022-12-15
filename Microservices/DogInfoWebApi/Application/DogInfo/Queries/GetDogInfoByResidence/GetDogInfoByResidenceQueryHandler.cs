using Application.Common.Exceptions;
using Application.DogInfo.DTOs;
using Application.DogInfo.VMs;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Application.DogInfo.Queries.GetDogInfoByResidence
{
    public sealed class GetDogInfoByResidenceQueryHandler : IRequestHandler<GetDogInfoByResidenceQuery, DogInfoListVM>
    {
        private readonly IConfiguration _configuration;

        public GetDogInfoByResidenceQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<DogInfoListVM> Handle(GetDogInfoByResidenceQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT id, name, residence, breed, is_disabled AS IsDisabled, approximate_weight AS ApproximateWeight, image_id AS ImageId FROM dogs_info " +
                "WHERE residence = @Residence";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                var result = await connection.QueryAsync<DogInfoLookupDTO>(query, new { Residence = request.Residence });

                if (!result.Any())
                {
                    throw new NotFoundException(request.Residence);
                }

                return new DogInfoListVM { DogsInfo = result.ToList() };
            }
        }
    }
}
