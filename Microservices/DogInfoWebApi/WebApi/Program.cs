using Application;
using Infrastructure;
using Infrastructure.Middlewares;
using Serilog;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, o) =>
{
    o.WriteTo.File(path: $@"{Directory.GetCurrentDirectory()}\Logs\log.txt");
    o.WriteTo.Console();
});

builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
