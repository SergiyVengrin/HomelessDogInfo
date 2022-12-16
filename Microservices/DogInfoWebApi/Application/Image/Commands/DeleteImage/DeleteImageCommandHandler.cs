using Application.Common.Exceptions;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Application.Image.Commands.DeleteImage
{
    public sealed class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Unit>
    {
        private readonly IConfiguration _configuration;

        public DeleteImageCommandHandler(IConfiguration congiguration)
        {
            _configuration = congiguration;
        }


        public async Task<Unit> Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            string path = "";

            var query = "SELECT image_path FROM images WHERE id = @Id";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                path = await connection.QuerySingleOrDefaultAsync<string>(query, new { Id = request.Id });

                if (string.IsNullOrEmpty(path))
                {
                    throw new NotFoundException(request.Id);
                }
            }

            File.Delete(path);

            query = "DELETE FROM images WHERE id = @Id";

            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                await connection.ExecuteAsync(query, new { Id = request.Id });
            }


            return Unit.Value;
        }
    }
}
