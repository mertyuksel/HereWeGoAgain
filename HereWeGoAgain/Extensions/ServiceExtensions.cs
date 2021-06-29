using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace HereWeGoAgain.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<RepositoryContext>
                (o => o.UseMySql(config["mysqlconnection:connectionString"],
                MySqlServerVersion.LatestSupportedServerVersion));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
