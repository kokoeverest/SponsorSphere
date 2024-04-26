using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SponsorSphere.Application;
using SponsorSphere.Application.App.Achievements.Commands;
using SponsorSphere.Application.App.Achievements.Queries;
using SponsorSphere.Application.App.Athletes.Commands;
using SponsorSphere.Application.App.Athletes.Queries;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
using SponsorSphere.Infrastructure;
using SponsorSphere.Infrastructure.Repositories;

var input = delegate (string s)
{
    Console.WriteLine(s);
    return Console.ReadLine();
};

static IMediator Init()
{
    var diContainer = new ServiceCollection()
        .AddDbContext<SponsorSphereDbContext>()
        .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly))
        .AddScoped<IUnitOfWork, UnitOfWork>()
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
        .AddAutoMapper(typeof(AssemblyMarker).Assembly)
        .BuildServiceProvider();

    return diContainer.GetRequiredService<IMediator>();
}

var mediator = Init();
// Simulation of a regisration form

//var name = input("First name: ");
//var lastName = input("Last name: ");
//var email = input("Email: ");
//var pass = input("Password: ");
//var country = input("Country: ");
//var phoneNumber = input("Phone: ");
//var birthDay = input("Date of birth: dd/mm/yyyy");

//var currentAthlete = await mediator.Send(new CreateAthleteCommand(
//    name,
//    lastName,
//    email,
//    pass,
//    country,
//    phoneNumber,
//    DateTime.Parse(birthDay),
//    SportsEnum.MountainRunning
//    ));

Console.WriteLine(await mediator.Send(new DeleteAthleteCommand(1)));

//var pesho = await mediator.Send(new GetAthleteByIdQuery(1));

//Console.WriteLine(pesho.LastName);
//pesho.LastName = "Ivankov";

//var updatedPesho = await mediator.Send(new UpdateAthleteCommand(pesho));


//async Task<AthleteDto> RegisterAthlete(Athlete athlete)
//{
//    return await mediator.Send(new CreateAthleteCommand(
//        athlete.Name,
//        athlete.LastName,
//        athlete.Email,
//        athlete.Password,
//        athlete.Country,
//        athlete.PhoneNumber,
//        athlete.BirthDate,
//        athlete.Sport
//        ));
//}

var athleteDtos = await mediator.Send(new GetAllAthletesQuery());
var peshoAchievements = await mediator.Send(new GetAchievementsByAthleteIdQuery(1));

foreach (var athlete in athleteDtos)
{
    Console.WriteLine($"Id: {athlete.Id}, " +
        $"Last name: {athlete.LastName}, " +
        $"Sport: {athlete.Sport}, " +
        $"Website: {athlete.Website}");
}
