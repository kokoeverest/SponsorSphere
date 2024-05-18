using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthleteByIdQuery(int AthleteId) : IRequest<AthleteDto>;

public class GetAthleteByIdQueryHandler : IRequestHandler<GetAthleteByIdQuery, AthleteDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAthleteByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AthleteDto> Handle(GetAthleteByIdQuery request, CancellationToken cancellationToken)
    {
        var athlete = await _unitOfWork.AthletesRepository.GetByIdAsync(request.AthleteId);
        var mappedAthlete = _mapper.Map<AthleteDto>(athlete);

        return await Task.FromResult(mappedAthlete);
    }
}