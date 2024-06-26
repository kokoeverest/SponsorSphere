﻿using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.SponsorIndividuals.Queries;

public record GetSponsorIndividualsByCountryQuery(CountryEnum Country, int PageNumber, int PageSize) : IRequest<List<SponsorIndividualDto>>;

public class GetSponsorIndividualsByCountryQueryHandler : IRequestHandler<GetSponsorIndividualsByCountryQuery, List<SponsorIndividualDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorIndividualsByCountryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorIndividualDto>> Handle(GetSponsorIndividualsByCountryQuery request, CancellationToken cancellationToken)
    {
        var sponsorIndividuals = await _unitOfWork.SponsorIndividualsRepository.GetByCountryAsync(request.Country, request.PageNumber, request.PageSize);
        var mappedSponsorIndividuals = _mapper.Map<List<SponsorIndividualDto>>(sponsorIndividuals);

        return await Task.FromResult(mappedSponsorIndividuals);
    }
}