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
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace Application
{
    public static class ConfigureServices
    {
        //! why , when i add service AddScope<IUnitOfWork,UnitOfWork>
        //!  automatically add object to method under?
        public static object AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestResponseMediateRDemo<,>));
            //reason to not AddScope<IRepository,Repository> is :
            //The Core Problem: Losing the Shared Context
            // The most important reason for favoring the Unit of Work registration is transactional integrity and shared context management.
            // Multiple Repositories, One Context: The UnitOfWork class in your example is designed to create and manage all repositories for a single HTTP request. 
            //It passes the same DbContext instance to every repository it creates.
            // Single Save Point: When you call _unitOfWork.SaveChanges(), it calls SaveChanges() on that one shared DbContext.
            //  This ensures that all changes made by any repository during the request are saved together as a single transaction. Either all succeed, or all fail.      
            services.AddScoped<IUnitOfWork, UnitofWork>();
            //services.AddDbContext<MustCreateDbContext>(option=>option.UseSqlite(Configuration.GetConnectionString("")))
            return services;
        }
//Use TRequest/TResponse when working directly with MediatR interfaces
//Use TInput/TOutput when creating abstract/base classes for better generalization


    }



}


