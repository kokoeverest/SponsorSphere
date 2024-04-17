﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SponsorSphere.Application;
using SponsorSphere.Application.App.Athletes.Commands;
using SponsorSphere.Application.App.Athletes.Queries;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;
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
    .AddAutoMapper(typeof(AssemblyMarker).Assembly)
    .BuildServiceProvider();

IMediator mediator = diContainer.GetRequiredService<IMediator>();
// Simulation of a regisration form

//var name = input("First name: ");
//var lastName = input("Last name: ");
//var email = input("Email: ");
//var pass = input("Password: ");
//var country = input("Country: ");
//var phoneNumber = input("Phone: ");
//var birthDay = input("Date of birth: dd/mm/yyyy");

//var currentAthlete = await mediator.Send(new CreateAthlete(
//    name,
//    lastName,
//    email,
//    pass,
//    country,
//    phoneNumber,
//    birthDay,
//    SportsEnum.MountainRunning
//    ));
var peshoAthlete = new Athlete(
    name: "Petar",
    lastName: "Petrov",
    email: "5rov@mail.mail",
    password: "dd",
    country: "bg",
    phoneNumber: "09198",
    birthDate: "30/09/1983",
    sport: SportsEnum.MountainRunning
    );

var goshoAthlete = new Athlete(
    "Georgi",
    "Petkov",
    "5kov@mail.mail",
    "ss",
    "bg",
    "09198",
    "30/03/2005",
    SportsEnum.Golf
    );

var sportEvent = new SportEvent(SportsEnum.MountainRunning, "Persenk ultra", EventsEnum.Race, "2020/08/16", "Bulgaria");

var peshoDto = await mediator.Send(new CreateAthlete(
    peshoAthlete.Name,
    peshoAthlete.LastName,
    peshoAthlete.Email,
    peshoAthlete.Password,
    peshoAthlete.Country,
    peshoAthlete.PhoneNumber,
    peshoAthlete.BirthDate.ToString(),
    peshoAthlete.Sport
    ));

var goshoDto = await mediator.Send(new CreateAthlete(
    goshoAthlete.Name,
    goshoAthlete.LastName,
    goshoAthlete.Email,
    goshoAthlete.Password,
    goshoAthlete.Country,
    goshoAthlete.PhoneNumber,
    goshoAthlete.BirthDate.ToString(),
    goshoAthlete.Sport
    ));


peshoAthlete.Achievements.Add(new Achievement(peshoAthlete, sportEvent, 1));

var athletes = await mediator.Send( new GetAllAthletes() );

foreach (var athlete in athletes)
{
    foreach (var achievement in athlete.Achievements)
    {
        Console.WriteLine(achievement.PlaceFinished);
    }
}
var golfers = await mediator.Send(new GetAthletesBySport(SportsEnum.Golf));

Console.WriteLine(string.Join("\n", golfers));