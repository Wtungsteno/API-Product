using ITS.Day2.Repositories.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITS.Day2.Repositories
{
    public static class Extensions
    {
        public static IServiceCollection AddAppRepository(this IServiceCollection services, string connectionString)
        {
            return services
                .AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString))
                .AddScoped<IAppRepository, AppRepository>();
        }
    }
}
