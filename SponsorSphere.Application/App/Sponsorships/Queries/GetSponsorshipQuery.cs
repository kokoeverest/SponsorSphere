using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsorships.Responses;
using SponsorSphere.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SponsorSphere.Application.App.Sponsorships.Queries;

public record GetSponsorshipQuery(int AthleteId, int SponsorId) : IRequest<SponsorshipDto>;

public class GetSponsorshipQueryHandler : IRequestHandler<GetSponsorshipQuery, SponsorshipDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetSponsorshipQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorshipDto> Handle(GetSponsorshipQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var sponsorship = await _unitOfWork.SponsorshipsRepository.GetSponsorshipAsync(request.AthleteId, request.SponsorId);
            var mappedSponsorships = _mapper.Map<SponsorshipDto>(sponsorship);

            return mappedSponsorships;
        }
        catch (Exception)
        {
            throw;
        }
    }
}