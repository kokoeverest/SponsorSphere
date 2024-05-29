using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Infrastructure.Options;
using SponsorSphere.Infrastructure.Repositories;

namespace SponsorSphere.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<SponsorSphereDbContext>((serviceProvider, options) =>
            {
                var dbOptions = serviceProvider.GetRequiredService<IOptions<DbOptions>>();

                options.UseSqlServer(dbOptions.Value.ConnectionString);
            });

            services.AddOptions<DbOptions>()
                .Configure<IConfiguration>((settings, configuration) =>
            {
                settings.ConnectionString = configuration["ConnectionString"] ?? string.Empty;
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>()
                    .AddScoped<IAchievementRepository, AchievementsRepository>()
                    .AddScoped<IAthleteRepository, AthleteRepository>()
                    .AddScoped<IBlogPostRepository, BlogPostRepository>()
                    .AddScoped<IGoalRepository, GoalRepository>()
                    .AddScoped<IPictureRepository, PictureRepository>()
                    .AddScoped<ISponsorCompanyRepository, SponsorCompanyRepository>()
                    .AddScoped<ISponsorRepository, SponsorRepository>()
                    .AddScoped<ISponsorIndividualRepository, SponsorIndividualRepository>()
                    .AddScoped<ISponsorshipRepository, SponsorshipRepository>()
                    .AddScoped<ISportEventRepository, SportEventRepository>()
                    .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use Always for HTTPS
                options.Cookie.SameSite = SameSiteMode.None; // Set SameSite to None for cross-site
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            });
        }
    }
}
