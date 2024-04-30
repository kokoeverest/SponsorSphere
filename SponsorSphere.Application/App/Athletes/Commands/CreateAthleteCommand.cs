using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record CreateAthleteCommand(Athlete Athlete) : IRequest<AthleteDto>;

public class CreateAthleteCommandHandler : IRequestHandler<CreateAthleteCommand, AthleteDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateAthleteCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AthleteDto> Handle(CreateAthleteCommand request, CancellationToken cancellationToken)
    {
        
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var newAthlete = await _unitOfWork.AthletesRepository.CreateAsync(request.Athlete);
            await _unitOfWork.CommitTransactionAsync();

            var athleteDto = _mapper.Map<AthleteDto>(newAthlete);
            return await Task.FromResult(athleteDto);
        }

        catch (InvalidDataException e)
        {
            await Console.Out.WriteLineAsync(e.Message);
            throw;
        }

        catch (Exception ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}