﻿using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;

namespace SponsorSphere.Application.App.Sponsorships.Queries;

public record GetSponsorshipsByLevelQuery(SponsorshipLevel Level, int PageNumber, int PageSize) : IRequest<List<SponsorshipDto>>;

public class GetSponsorshipsByLevelQueryHandler : IRequestHandler<GetSponsorshipsByLevelQuery, List<SponsorshipDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorshipsByLevelQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<List<SponsorshipDto>> Handle(GetSponsorshipsByLevelQuery request, CancellationToken cancellationToken)
    {
        var sponsorships = await _unitOfWork.SponsorshipsRepository.GetByLevelAsync(request.Level, request.PageNumber, request.PageSize);
        var mappedSponsorships = _mapper.Map<List<SponsorshipDto>>(sponsorships);
        return mappedSponsorships;
    }
}