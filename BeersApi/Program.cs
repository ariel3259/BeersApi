using BeersApi.Context;
using BeersApi.Models;
using BeersApi.Repositories;
using BeersApi.Repositories.Interfaces;
using BeersApi.Services;
using BeersApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string? connectionString = builder.Configuration["ConnectionStrings:Dev"];

if (connectionString == null) throw new Exception("connection string needed");
builder.Services.AddDbContext<ApplicationContext>((options) =>
{
    options.UseSqlite(connectionString);
},ServiceLifetime.Transient);

//Add repositories
builder.Services.AddTransient<IRepository<DrinkTypes>, DrinkTypesRepository>();
builder.Services.AddTransient<ICrudRepository<Drinks>, DrinksCrudRepository>();

//Add Services
builder.Services.AddTransient<IDrinkTypesService, DrinkTypesService>();

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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }