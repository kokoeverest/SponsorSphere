using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SponsorSphere.Application;
using SponsorSphere.Application.App.Athletes.Commands;
using SponsorSphere.Application.App.Athletes.Queries;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Infrastructure.Interfaces;
using SponsorSphere.Infrastructure.Repositories;

var input = delegate (string s)
{
    Console.WriteLine(s);
    return Console.ReadLine();
};


var diContainer = new ServiceCollection()
    .AddSingleton<IAthleteRepository, AthleteRepository>()
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(AssemblyMarker).Assembly))
    .BuildServiceProvider();

IMediator mediator = diContainer.GetRequiredService<IMediator>();
// Simulation of a regisration form

//var name = input("First name: ");
//var lastName = input("Last name: ");
//var email = input("Email: ");
//var pass = input("Password: ");
//var country = input("Country: ");
//var phone = input("Phone: ");
//var birthDay = input("Date of birth: dd/mm/yyyy");

//var currentAthlete = await mediator.Send(new CreateAthlete(
//    name,
//    lastName,
//    email,
//    pass,
//    country,
//    phone,
//    birthDay,
//    SportsEnum.MountainRunning
//    ));

var peshoAthlete = await mediator.Send(new CreateAthlete(
    "Petar",
    "Petrov",
    "5rov@mail.mail",
    "dd",
    "bg",
    "09198",
    "30/09/1983",
    SportsEnum.MountainRunning
    ));

var goshoAhtlete = await mediator.Send(new CreateAthlete(
    "Georgi",
    "Petkov",
    "5kov@mail.mail",
    "ss",
    "bg",
    "09198",
    "30/03/2005",
    SportsEnum.Golf
    ));

var athletes = await mediator.Send( new GetAllAthletes() );
var golfers = await mediator.Send(new GetAthletesBySport(SportsEnum.Golf));

Console.WriteLine(string.Join("\n", golfers));