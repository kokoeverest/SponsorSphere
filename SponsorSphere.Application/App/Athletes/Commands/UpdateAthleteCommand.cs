using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record UpdateAthleteCommand(AthleteDto AthleteToUpdate) : IRequest<AthleteDto>;

public class UpdateAthleteCommandHandler : IRequestHandler<UpdateAthleteCommand, AthleteDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAthleteCommandHandler> _logger;

    public UpdateAthleteCommandHandler(ILogger<UpdateAthleteCommandHandler> logger, UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<AthleteDto> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var start = DateTime.Now;
            _logger.LogInformation("Action: {Action}", request.ToString());

            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.AthletesRepository.UpdateAsync(request.AthleteToUpdate);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return result;
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    } 
}
