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
                var result = await connection.QueryAsync<Domain.DogInfo>(query);

                List<DogInfoLookupDTO> mappedResult = new List<DogInfoLookupDTO>();

                foreach (var r in result)
                {
                    mappedResult.Add(_mapper.Map<DogInfoLookupDTO>(r));
                }

                return new DogInfoListVM { DogsInfo = mappedResult};
            }
        }
    }
}
