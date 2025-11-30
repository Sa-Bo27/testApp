using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechnicalExercice.API;
using TechnicalExercice.Application;
using TechnicalExercice.Domain.Interface;
using TechnicalExercice.Domain.Models;
using TechnicalExercice.Infrastructure.Context;
using TechnicalExercice.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("TechnicalExercice") ?? "Data Source=TechnicalExercice.db";
//builder.Services.AddSqlite<MyDbContext>(connectionString);
builder.Services.AddDbContext<MyDbContext>(options => options.UseInMemoryDatabase("MyDB"));
builder.Services.AddScoped<IclientService, ClientService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IShoppingService, ShoppingService>();
// Register application services
builder.Services.AddScoped<CalculateTotalBasket>();
builder.Services.AddScoped<CreateClient>();
builder.Services.AddScoped<AddProductToBasket>();
builder.Services.AddScoped<GetClients>();
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");


app.MapEndpoints();

app.Run();
record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

// Expose Program class for functional tests (WebApplicationFactory<Program>)
public partial class Program { }
