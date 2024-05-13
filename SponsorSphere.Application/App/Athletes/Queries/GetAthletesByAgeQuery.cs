using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesByAgeQuery(int Age, int PageNumber, int PageSize) : IRequest<List<AthleteDto>>;

public class GetAthletesByAgeQueryHandler : IRequestHandler<GetAthletesByAgeQuery, List<AthleteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAthletesByAgeQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AthleteDto>> Handle(GetAthletesByAgeQuery request, CancellationToken cancellationToken)
    {
        var athletes = await _unitOfWork.AthletesRepository.GetByAgeAsync(request.Age, request.PageNumber, request.PageSize);
        var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        return await Task.FromResult(mappedAthletes);
    }
}