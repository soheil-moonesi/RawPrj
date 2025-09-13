/** The WebApplicationBuilder has these main properties:
 Configuration - Configuration system (IConfiguration)
 Environment - Hosting environment information (IWebHostEnvironment)
 Logging - Logging configuration (ILoggingBuilder)
 Services - Dependency injection container (IServiceCollection)
 WebHost - Web host configuration (IWebHostBuilder)
 Host - Generic host configuration (IHostBuilder) */

using Application;
using AsciiArtSvc;
using CompositionRoot.DemoFeature;
using Figgle;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static CompositionRoot.DemoFeature.MyFeature;
//System.Reflection
// Purpose: Allows you to inspect and interact with your application's structure at runtime.
// What it does:
// Examines assemblies, types, methods, and properties
// Discovers what resources are embedded in your application
// Gets information about your compiled code
// Find what resources are available in your assembly
// todo: test code
// string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
using System.Reflection;
//System.Resources
// Purpose: Manages embedded resources like strings, images, and files in your application.
// What it does:
// Loads and accesses compiled resources (.resources files)
// Handles resource fallback for different languages
// Manages resource sets and streams
//todo : test code
// Create a ResourceManager to access embedded resources
//var resourceManager = new ResourceManager("MyApp.Resources", assembly);
// Read string values from resources
//string welcomeMessage = resourceManager.GetString("WelcomeMessage");
using System.Resources;

//System.Globalization
// Purpose: Handles language, culture, and region-specific formatting.
// What it does:
// Manages different cultures and languages (en-US, fr-FR, etc.)
// Handles date, time, number, and currency formatting
// Supports internationalization and localization
//todo: test code
// Set specific culture for resource lookup
// var frenchCulture = new CultureInfo("fr-FR");
// string frenchText = resourceManager.GetString("WelcomeMessage", frenchCulture);

 // Culture-specific formatting
// double price = 1234.56;
// string usPrice = price.ToString("C", new CultureInfo("en-US")); // "$1,234.56"
// string frPrice = price.ToString("C", new CultureInfo("fr-FR")); // "1 234,56 €"


using System.Globalization;


//todo : add services
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

//tip: builder --(register dependencies)   -- App

//-------
//1.unchained version
// builder.Services.AddSingleton<MyFeature>();
// builder.Services.AddSingleton<IMyFeatureDependency, MyFeatureDependency>();

//2.chained version
// builder.Services.AddSingleton<MyFeature>()
// .AddSingleton<IMyFeatureDependency, MyFeatureDependency>();

//3.best practice
builder.Services.AddDemoFeature();


builder.Services.AddApplicationServices();
//------

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

//todo: add connection string for sqlite database 
//! AddInfrastructureServices 
//* we must inject builder.configuration to use for infrastructure for example to database option 
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
//tip: app --(IOC container is now available)-- App.run


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




//http://localhost:5026/MediateRTest?a=5&b=3
app.MapGet("/MediateRTest", async (IMediator mediateR, [FromQuery] int a, [FromQuery] int b) =>
{
var query = new AddNumbersQuery(a, b);
var result = await mediateR.Send(query);
Console.WriteLine($"{result} in Program");

})
.WithName("MediateRTest");
//todo: create other types of requests in mediateR
//todo: use resource in controllers
//https://www.c-sharpcorner.com/UploadFile/65794e/how-to-read-resx-file-in-c-sharp/
ResourceManager resourceManager = new ResourceManager("MyApiPrj.MyResources", Assembly.GetExecutingAssembly());
string MyResourcesLog = resourceManager.GetString("MyResourseLog");
Console.WriteLine(MyResourcesLog);
//app.MapGet("/{text}", (string text) => FiggleFonts.Standard.Render(text));
//http://localhost:5026/Soheil?font=DotMatrix pass font by query string
app.MapGet("/{text}", (string text, string? font) => AsciiArt.Write(text, font));



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}


// The appsettings.json file is loaded first. Then the values in appsettings.<ASPNETCORE_
// ENVIRONMENT>.json are loaded. The latest configuration values loaded in the chain
// override the previous values in which the pathname matches.
