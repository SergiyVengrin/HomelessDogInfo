using Application.Common.Exceptions;
using Application.DogInfo.DTOs;
using Application.DogInfo.VMs;
using AutoMapper;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Application.DogInfo.Queries.GetDogInfoList
{
    public sealed class GetDogInfoListQueryHandler : IRequestHandler<GetDogInfoListQuery, DogInfoListVM>
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GetDogInfoListQueryHandler(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<DogInfoListVM> Handle(GetDogInfoListQuery request, CancellationToken cancellationToken)
        {
            var query = "SELECT id, name, residence, breed, is_disabled AS IsDisabled, approximate_weight AS ApproximateWeight, image_id AS ImageId FROM dogs_info";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                var result = await connection.QueryAsync<DogInfoLookupDTO>(query);

                if (!result.Any())
                {
                    throw new NotFoundException();
                }

                return new DogInfoListVM { DogsInfo = result.ToList() };
            }
        }
    }
}
