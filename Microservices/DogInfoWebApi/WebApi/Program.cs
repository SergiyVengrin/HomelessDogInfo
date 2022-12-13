using Application;
using Infrastructure.Middlewares;
using Serilog;
using WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, o) =>
{
    o.WriteTo.File(path: $@"{Directory.GetCurrentDirectory()}\Logs\log.txt");
    o.WriteTo.Console();
});

// Add services to the container.
builder.Services.AddApplication();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
builder.Services.AddTransient<ExceptionHandlingMiddleware>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
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
