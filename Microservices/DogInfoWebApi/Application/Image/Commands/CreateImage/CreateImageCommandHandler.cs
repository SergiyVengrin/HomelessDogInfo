using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Application.Image.Commands.CreateImage
{
    public sealed class CreateImageCommandHandler : IRequestHandler<CreateImageCommand, int>
    {
        private readonly IConfiguration _configuration;

        public CreateImageCommandHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<int> Handle(CreateImageCommand request, CancellationToken cancellationToken)
        {
            int id;

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles/Images");
            string imageName = request.Image.FileName;
            imagePath = Path.Combine(imagePath, imageName);


            var query = "INSERT INTO images (image_name, image_path) VALUES (@ImageName, @ImagePath) RETURNING images.id"; 
            using (var connection = new NpgsqlConnection(_configuration.GetConnectionString("DogInfoDbConnectionString")))
            {
                await connection.OpenAsync(cancellationToken);
                id =  await connection.QuerySingleAsync<int>(query, new { ImageName = imageName, ImagePath = imagePath });
            }


            using (FileStream fileStream = File.Create(imagePath))
            {
                await request.Image.CopyToAsync(fileStream, cancellationToken);
            }

            return id;
        }
    }
}
