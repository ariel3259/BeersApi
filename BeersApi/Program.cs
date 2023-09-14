using BeersApi.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string? connectionString = builder.Configuration["ConnectionStrings:Dev"];

if (connectionString == null) throw new Exception("connection string needed");
builder.Services.AddDbContext<ApplicationContext>((options) =>
{
    options.UseSqlite(connectionString);
},ServiceLifetime.Transient);

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
