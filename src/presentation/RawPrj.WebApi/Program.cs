using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RawPrj.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<TravelDbContext>(options=> options
.UseSqlite("DataSource=./TravelTourDatabase.db"));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" }));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    //is redundent with useSwagger
    //app.MapOpenApi();
    // http://localhost:5026/swagger/v1/swagger.json
   //http://localhost:5026/swagger/Index.html
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
     c=>c.SwaggerEndpoint("/swagger/v1/swagger.json","Web v1")
    );
}


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
    var forecast =  Enumerable.Range(1, 5).Select(index =>
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

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
