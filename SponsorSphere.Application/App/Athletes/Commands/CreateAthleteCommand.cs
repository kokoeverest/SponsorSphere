using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record CreateAthleteCommand(RegisterAthleteDto Athlete) : IRequest<AthleteDto>;

public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand, AthleteDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    public CreateAthleteCommandHandler(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<AthleteDto> Handle(CreateAthleteCommand request, CancellationToken cancellationToken)
    {
        var athlete = _mapper.Map<Athlete>(request.Athlete);

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var newAthlete = await _userManager.CreateAsync(athlete, request.Athlete.Password);

            var result = await _userManager.AddToRoleAsync(athlete, RoleConstants.Athlete);

            await _unitOfWork.CommitTransactionAsync();
            var mapped = _mapper.Map<AthleteDto>(athlete);

            return await Task.FromResult(mapped);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}