//register dependencies with -> Add[Feature Name] like AddApplicationServices

//if Feature become too big or doestoo many things or
// start to share dependecies with other feature -> rethink for Arcituecture

// To implement this pattern, we use extension methods, making it trivial. Here’s a guide:
// 1. Create a static class named [subject]Extensions in the Microsoft.Extensions.
// DependencyInjection namespace.
// 2. Create an extension method that returns the IServiceCollection interface, which allows
// method calls to be chained.

// According to Microsoft’s recommendation, we should create 
//the class in the same namespace as the element we extend.

using System.Reflection;
using Application.Common.Behaviour;
using MediatR;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestResponseMediateRDemo<,>));


            return services;
        }



    }



}


