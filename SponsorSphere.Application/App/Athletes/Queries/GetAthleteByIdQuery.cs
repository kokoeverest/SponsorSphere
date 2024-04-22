using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthleteByIdQuery(int AthleteId) : IRequest<Athlete?>;

public class GetAthleteByIdQueryHandler : IRequestHandler<GetAthleteByIdQuery, Athlete?>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAthleteByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Athlete?> Handle(GetAthleteByIdQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.AthletesRepository.GetByIdAsync(request.AthleteId);
        //var mappedAthlete = _mapper.Map<AthleteDto>(athlete);

        //return await Task.FromResult(mappedAthlete);
    }
}