using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SponsorSphere.Application;
using SponsorSphere.Application.App.Athletes.Commands;
using SponsorSphere.Application.App.Athletes.Queries;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.ConsolePresentation;
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

var athletes = Seeder.SeedAthletes();
var sponsorCompanies = Seeder.SeedSponsorCompanies();
var sportevents = Seeder.SeedSportEvents();

AthleteDto? peshoDto = null;

try
{
    peshoDto = await RegisterAthlete(athletes[0]);
    //var goshoDto = RegisterAthlete(goshoAthlete);
}
catch (InvalidDataException e)
{
    Console.WriteLine(e.Message);
}

var pesho = await mediator.Send(new GetAthleteByIdQuery(12));
if (pesho != null)
    await mediator.Send(
        new UpdateAthleteCommand(
            AthleteToUpdate: 12,
            NewWebsite: "pesho.con",
            NewFaceBookLink: "",
            NewStravaLink: "strava.con/peshoatleta",
            NewInstagramLink: "",
            NewTwitterLink: ""));

async Task<AthleteDto> RegisterAthlete(Athlete athlete)
{
    return await mediator.Send(new CreateAthleteCommand(
        athlete.Name,
        athlete.LastName,
        athlete.Email,
        athlete.Password,
        athlete.Country,
        athlete.PhoneNumber,
        athlete.BirthDate,
        athlete.Sport
        ));
}
//Console.WriteLine(await mediator.Send(new DeleteAthleteCommand(1)));

var athleteDtos = await mediator.Send(new GetAllAthletesQuery());

foreach (var athlete in athleteDtos)
{
    Console.WriteLine($"Id: {athlete.Id}, " +
        $"Last name: {athlete.LastName}, " +
        $"Sport: {athlete.Sport}, " +
        $"Website: {athlete.Website}");
}
var golfers = await mediator.Send(new GetAthletesBySportQuery(SportsEnum.Golf));

Console.WriteLine(golfers[0].Age);