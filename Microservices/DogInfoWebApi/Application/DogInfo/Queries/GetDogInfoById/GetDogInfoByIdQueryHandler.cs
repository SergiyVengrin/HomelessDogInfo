using Application.Common.Exceptions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Application.DogInfo.Queries.GetDogInfoById
{
    public sealed class GetDogInfoByIdQueryHandler : IRequestHandler<GetDogInfoByIdQuery, DogInfoVM>
    {
        private readonly IConfiguration _configuration;

        public GetDogInfoByIdQueryHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<DogInfoVM> Handle(GetDogInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT id, name, residence, breed, is_disabled AS IsDisabled, approximate_weight AS ApproximateWeight, image_id AS ImageId FROM dogs_info " +
                "WHERE id = @Id";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                var result = await connection.QuerySingleOrDefaultAsync<DogInfoVM>(query, new { Id = request.Id });

                if (result is null)
                {
                    throw new NotFoundException(request.Id);
                }

                return result;
            }
        }
    }
}
