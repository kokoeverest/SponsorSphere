﻿using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Athletes.Responses;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Athletes.Queries;

public record GetAthletesByCountryQuery(string Country) : IRequest<List<AthleteDto>>;

public class GetAthletesByCountryQueryHandler : IRequestHandler<GetAthletesByCountryQuery, List<AthleteDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAthletesByCountryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<AthleteDto>> Handle(GetAthletesByCountryQuery request, CancellationToken cancellationToken)
    {
        var athletes = await _unitOfWork.AthletesRepository.GetByCountryAsync(request.Country);
        var mappedAthletes = _mapper.Map<List<AthleteDto>>(athletes);

        return await Task.FromResult(mappedAthletes);
    }
}