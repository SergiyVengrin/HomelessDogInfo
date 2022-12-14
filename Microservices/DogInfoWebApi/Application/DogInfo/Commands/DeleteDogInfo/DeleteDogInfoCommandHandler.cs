using Dapper;
using Application.Common.Exceptions;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Application.DogInfo.Commands.DeleteDogInfo
{
    internal class DeleteDogInfoCommandHandler : IRequestHandler<DeleteDogInfoCommand>
    {
        private readonly IConfiguration _configuration;

        public DeleteDogInfoCommandHandler(IConfiguration congiguration)
        {
            _configuration = congiguration;
        }


        public async Task<Unit> Handle(DeleteDogInfoCommand request, CancellationToken cancellationToken)
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


            query = "DELETE FROM dogs_info WHERE id = @Id";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync(query, new {Id = request.Id});
            }

            return Unit.Value;
        }
    }
}
