//using AutoMapper;
//using MediatR;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using SponsorSphere.Application;
//using SponsorSphere.Application.Interfaces;
//using SponsorSphere.Domain.Models;
//using SponsorSphere.Infrastructure;
//using SponsorSphere.Infrastructure.Repositories;
//using System;

//namespace AmdarisEshop.IntegrationTests.Helpers
//{
//    public static class TestHelpers
//    {
//        public static IMapper CreateMapper()
//        {
//            var services = new ServiceCollection();
//            services.AddAutoMapper(typeof(AssemblyMarker));
//            var serviceProvider = services.BuildServiceProvider();
//            return serviceProvider.GetRequiredService<IMapper>();
//        }

//        public static IMediator CreateMediator(IUnitOfWork unitOfWork)
//        {
//            var services = new ServiceCollection();
//            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly));
//            services.AddSingleton(unitOfWork);
//            services.AddScoped<IUnitOfWork, UnitOfWork>()
//                    .AddScoped<IAchievementRepository, AchievementsRepository>()
//                    .AddScoped<IAthleteRepository, AthleteRepository>()
//                    .AddScoped<IBlogPostRepository, BlogPostRepository>()
//                    .AddScoped<IGoalRepository, GoalRepository>()
//                    .AddScoped<IPictureRepository, PictureRepository>()
//                    .AddScoped<ISponsorCompanyRepository, SponsorCompanyRepository>()
//                    .AddScoped<ISponsorRepository, SponsorRepository>()
//                    .AddScoped<ISponsorIndividualRepository, SponsorIndividualRepository>()
//                    .AddScoped<ISponsorshipRepository, SponsorshipRepository>()
//                    .AddScoped<ISportEventRepository, SportEventRepository>();

//            var serviceProvider = services.BuildServiceProvider();
//            return serviceProvider.GetRequiredService<IMediator>();
//        }

//        public static UserManager<User> CreateUserManager()
//        {
//            var services = new ServiceCollection();

//            // Add required services
//            services.AddLogging();
//            services.AddDbContext<SponsorSphereDbContext>(options =>
//                options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

//            services.AddIdentityCore<User>(options => { })
//                .AddRoles<IdentityRole>()
//                .AddEntityFrameworkStores<SponsorSphereDbContext>()
//                .AddDefaultTokenProviders();

//            services.AddScoped<IUserStore<User>, MockUserStore>();
//            services.AddScoped<IRoleStore<IdentityRole>, RoleStore<IdentityRole, SponsorSphereDbContext>>();

//            var serviceProvider = services.BuildServiceProvider();

//            return serviceProvider.GetRequiredService<UserManager<User>>();
//        }
//    }
//}
//// Mock user store implementation for testing
//public class MockUserStore : IUserStore<User>, IUserPasswordStore<User>, IUserSecurityStampStore<User>
//    {
//        // Implement the necessary methods for IUserStore, IUserPasswordStore, IUserSecurityStampStore
//        public Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken) => Task.FromResult(IdentityResult.Success);
//        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken) => Task.FromResult(IdentityResult.Success);
//        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken) => Task.FromResult(new User());
//        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) => Task.FromResult(new User());
//        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.UserName);
//        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.Id.ToString());
//        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) => Task.FromResult(user.UserName);
//        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken) => Task.CompletedTask;
//        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken) => Task.CompletedTask;
//        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken) => Task.FromResult(IdentityResult.Success);
//        public void Dispose() { }

//        // Implement other necessary methods for IUserPasswordStore and IUserSecurityStampStore
//        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken) => Task.CompletedTask;
//        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) => Task.FromResult("hashedPassword");
//        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) => Task.FromResult(true);
//        public Task SetSecurityStampAsync(User user, string stamp, CancellationToken cancellationToken) => Task.CompletedTask;
//        public Task<string> GetSecurityStampAsync(User user, CancellationToken cancellationToken) => Task.FromResult("securityStamp");
//    }

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SponsorSphere.Application;
using SponsorSphere.Infrastructure;
using System.Xml.Linq;

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

            // Add required services
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
