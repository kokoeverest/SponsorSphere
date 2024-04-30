using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SponsorSphere.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Application;
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

            services.AddOptions<DbOptions>().Configure<IConfiguration>((settings, configuration) =>
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
                    .AddAutoMapper(typeof(AssemblyMarker).Assembly);
                    //.BuildServiceProvider();
        }
    }
}
