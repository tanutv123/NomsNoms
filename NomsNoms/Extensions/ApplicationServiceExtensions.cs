using Microsoft.EntityFrameworkCore;
using NomsNoms.Data;
using NomsNoms.Helpers;

namespace NomsNoms.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //Add DBContext
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<LogUserActivity>();
            return services;
        }
    }
}
