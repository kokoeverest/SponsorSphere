using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAllAthletesQuery(int PageNumber, int PageSize) : IRequest<List<AthleteDto>>;

public class GetAllAthletesQueryHandler : IRequestHandler<GetAllAthletesQuery, List<AthleteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllAthletesQueryHandler> _logger;

    public GetAllAthletesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<GetAllAthletesQueryHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<AthleteDto>> Handle(GetAllAthletesQuery request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());
        var athletes = await _unitOfWork.AthletesRepository.GetAllAsync(request.PageNumber, request.PageSize);
        var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        return await Task.FromResult(mappedAthletes);
    }
}