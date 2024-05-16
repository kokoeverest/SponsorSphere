using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthleteByIdQuery(int AthleteId) : IRequest<AthleteDto>;

public class GetAthleteByIdQueryHandler : IRequestHandler<GetAthleteByIdQuery, AthleteDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAthleteByIdQueryHandler> _logger;

    public GetAthleteByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAthleteByIdQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AthleteDto> Handle(GetAthleteByIdQuery request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        var athlete = await _unitOfWork.AthletesRepository.GetByIdAsync(request.AthleteId);
        var mappedAthlete = _mapper.Map<AthleteDto>(athlete);

        _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        return await Task.FromResult(mappedAthlete);
    }
}