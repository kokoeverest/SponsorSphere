using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SponsorSphere.Application;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure;
using SponsorSphere.Infrastructure.Repositories;

namespace SponsorSphere.IntegrationTests.Helpers
{
    public static class TestHelpers
    {
        public static IMapper CreateMapper()
        {
            var services = new ServiceCollection();
            services.AddAutoMapper(typeof(AssemblyMarker));
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IMapper>();
        }

        public static IMediator CreateMediator(IUnitOfWork unitOfWork)
        {
            var services = new ServiceCollection();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));

            services.AddDbContext<SponsorSphereDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: "TestDatabase")
                .EnableSensitiveDataLogging();
            });

            services.AddAutoMapper(typeof(AssemblyMarker));


            services.AddSingleton(unitOfWork);
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
                    .AddScoped<ISportEventRepository, SportEventRepository>();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IMediator>();
        }

        public static UserManager<User> CreateUserManager()
        {
            var services = new ServiceCollection();

            services.AddLogging();
            services.AddDbContext<SponsorSphereDbContext>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddDataProtection();

            services.AddIdentityCore<User>(options => { })
                .AddRoles<IdentityRole<int>>()
                .AddEntityFrameworkStores<SponsorSphereDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserStore<User>, UserStore<User, IdentityRole<int>, SponsorSphereDbContext, int>>();
            services.AddScoped<IRoleStore<IdentityRole<int>>, RoleStore<IdentityRole<int>, SponsorSphereDbContext, int>>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.GetRequiredService<UserManager<User>>();
        }
    }
}
