using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;

namespace SponsorSphere.Infrastructure.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        internal static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<SponsorSphereDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DbOptions>>();

                options.UseSqlServer(dbOptions.Value.ConnectionString);
            });

            services.AddIdentityApiEndpoints<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<UserRole>()
            .AddEntityFrameworkStores<SponsorSphereDbContext>();

            services.AddOptions<DbOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                settings.ConnectionString = configuration["ConnectionString"] ?? string.Empty;
            });

        }
    }
}
