using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    }
}
