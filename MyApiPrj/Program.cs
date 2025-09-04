/** The WebApplicationBuilder has these main properties:
 Configuration - Configuration system (IConfiguration)
 Environment - Hosting environment information (IWebHostEnvironment)
 Logging - Logging configuration (ILoggingBuilder)
 Services - Dependency injection container (IServiceCollection)
 WebHost - Web host configuration (IWebHostBuilder)
 Host - Generic host configuration (IHostBuilder) */

using Figgle;

//When you create a web app, IConfiguration is automatically set up
var builder = WebApplication.CreateBuilder(args);
//IConfiguration ---
var appSettingLogging = builder.Configuration["Logging:LogLevel:Default"];
Console.WriteLine(appSettingLogging);

//dotnet run --myArg to write myArgs in terminal 
//dotnet run arg1 arg2 arg3

//Console.WriteLine(args[0]);

//IConfiguration ***

//Enviroment ---
//Enviroment get info from properties/lunchSetting.json/Profile/ASPNETCORE_ENVIRONMENT
//ASPNETCORE_ENVIRONMENT=Development
//ASPNETCORE_ENVIRONMENT=Production
//ASPNETCORE_ENVIRONMENT=Staging



if (builder.Environment.IsDevelopment())
{
    //when we use builder.Logging.ClearProviders(); to remove all logging defualts this action
    //is not take effect on Console because Console.writeline bypass all loging systems 
    //and not affected by builder.logging
    Console.WriteLine("we are in Dev Enviroment");
}

//Enviroment ***

// Logging ---

//logging is already installed but if we want to custumize it 
//1.delete all defualt
//builder.Logging.ClearProviders();

//2.Add Console log
//builder.Logging.AddConsole();


// usecase of SetMinimumLevel As a Safety Net in Library Code
// If you are writing a library and want to ensure it never logs Trace messages unless the consuming application explicitly opts in, you could set a higher floor.
// csharp

// Inside a library's service registration
// services.AddLogging(loggingBuilder => {
//     loggingBuilder.SetMinimumLevel(LogLevel.Information); // Library won't log Debug/Trace by itself
// });


// SetMinimumLevel(LogLevel.Debug) is like setting a filter that says:
//Only show me log messages that are Debug level or higher
//  Log Level	Severity	Shows When Set to Debug?
//  Trace	    Lowest	        ❌ No (filtered out)
//  Debug	    Low         	✅ Yes
//  Information	Medium   	    ✅ Yes
//  Warning	    High	        ✅ Yes
//  Error	    Higher       	✅ Yes
//  Critical	    Highest	        ✅ Yes
//builder.Logging.SetMinimumLevel(LogLevel.Debug);
//builder.Logging.SetMinimumLevel(LogLevel.Trace);

//Logging ***

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.Logger.LogTrace("this is log trace");

//log critical error
app.Logger.LogCritical("this is critical logging message");

//if SetMinimumLevel = debug , trace log is not show

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

app.MapGet("/{text}", (string text) => FiggleFonts.Standard.Render(text));

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


// The appsettings.json file is loaded first. Then the values in appsettings.<ASPNETCORE_
// ENVIRONMENT>.json are loaded. The latest configuration values loaded in the chain
// override the previous values in which the pathname matches.