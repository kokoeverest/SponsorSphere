using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsorships.Dtos;
using SponsorSphere.Application.Interfaces;

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
            var mappedSponsorship = _mapper.Map<SponsorshipDto>(sponsorship);

            return mappedSponsorship;
        }
        catch (Exception)
        {
            throw;
        }
    }
}