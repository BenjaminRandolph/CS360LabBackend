using Microsoft.EntityFrameworkCore;
using Lab_E_Commerce_Website_API.Models;
using Lab_E_Commerce_Website_API;

var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
builder.WebHost.UseUrls($"http://*:{port}");

var databaseURL = Environment.GetEnvironmentVariable("PGHOST");
var databasePort = Environment.GetEnvironmentVariable("PGPORT");
var databaseName = Environment.GetEnvironmentVariable("PGDATABASE");
var databaseUser = Environment.GetEnvironmentVariable("PGUSER");
var databasePassword = Environment.GetEnvironmentVariable("PGPASSWORD");

// Add services to the container.

builder.Services.AddControllers();
//                                                        UseNpgsql(builder.Configuration.GetConnectionString("PostgresqlDatabase"))
builder.Services.AddDbContext<DatabaseContext>(opt => opt.UseNpgsql("Host=" + databaseURL + databasePort + ";Database=" + databaseName + ";Username=" + databaseUser + ";Password=" + databasePassword));
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

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();