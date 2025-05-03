using Microsoft.EntityFrameworkCore;
using Lab_E_Commerce_Website_API.Models;
using Lab_E_Commerce_Website_API;

var builder = WebApplication.CreateBuilder(args);

//var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
//builder.WebHost.UseUrls($"http://bensfunnyapi:{port}");

var databaseURL = Environment.GetEnvironmentVariable("PGHOST") ?? "localhost";
var databasePort = Environment.GetEnvironmentVariable("PGPORT") ?? ":6000";
var databaseName = Environment.GetEnvironmentVariable("PGDATABASE") ?? "ECommerceLab";
var databaseUser = Environment.GetEnvironmentVariable("PGUSER") ?? "postgres";
var databasePassword = Environment.GetEnvironmentVariable("PGPASSWORD") ?? "P@ssw0rd!";

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