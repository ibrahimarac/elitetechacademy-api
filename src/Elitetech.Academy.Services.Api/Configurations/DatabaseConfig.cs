using Elitetech.Academy.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Elitetech.Academy.Services.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if(services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<EliteContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("EliteConnection"));                
            });
        }
    }
}
