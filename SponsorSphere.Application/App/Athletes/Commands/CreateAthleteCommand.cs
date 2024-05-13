using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
    private readonly ILogger<CreateAthleteCommandHandler> _logger;
    public CreateAthleteCommandHandler(UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreateAthleteCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AthleteDto> Handle(CreateAthleteCommand request, CancellationToken cancellationToken)
    {
        var athlete = _mapper.Map<Athlete>(request.Athlete);

        try
        {
            var start = DateTime.Now;
            _logger.LogInformation("Action: {Action}", request.ToString());

            await _unitOfWork.BeginTransactionAsync();

            var newAthlete = await _userManager.CreateAsync(athlete, request.Athlete.Password);
            var result = await _userManager.AddToRoleAsync(athlete, RoleConstants.Athlete);

            if (!result.Succeeded) 
            {
                throw new InvalidDataException();
            }

            await _unitOfWork.CommitTransactionAsync();
            var mapped = _mapper.Map<AthleteDto>(athlete);

            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(mapped);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError("Action: {Action} failed", request.ToString());
            throw;
        }
    }
}