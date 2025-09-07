
//! it is better to have one name "ConfigureServices" for application namespace and infrastructure namespace

using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

           services.AddDbContext<ApplicationDbContext>(option => option.UseSqlite(configuration.GetConnectionString("")));

            return services;

        }
    }
}
