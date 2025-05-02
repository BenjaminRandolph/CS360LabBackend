using Microsoft.EntityFrameworkCore;
using Lab_E_Commerce_Website_API.Models;
using Lab_E_Commerce_Website_API;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//                                                        UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlDatabase"))
builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseNpgsql("Host=localhost:6000;Database=ECommerceLab;Username=postgres;Password=P@ssw0rd!"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(builder =>
{
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();