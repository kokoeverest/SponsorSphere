using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.Sponsorships.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Sponsorships.Commands;

public record CreateSponsorshipCommand(
        DateTime Created,
        SponsorshipLevel Level,
        decimal Amount,
        int AthleteId,
        int SponsorId
    ) : IRequest<SponsorshipDto>;
public class CreateSponsorshipCommandHandler : IRequestHandler<CreateSponsorshipCommand, SponsorshipDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateSponsorshipCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorshipDto> Handle(CreateSponsorshipCommand request, CancellationToken cancellationToken)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var newSponsorship = _unitOfWork.SponsorshipsRepository
                .CreateAsync(new Sponsorship
                {
                    Created = request.Created,
                    Level = request.Level,
                    Amount = request.Amount,
                    AthleteId = request.AthleteId,
                    SponsorId = request.SponsorId

                });
            await _unitOfWork.CommitTransactionAsync();
            var mappedSponsorship = _mapper.Map<SponsorshipDto>( newSponsorship );
            return await Task.FromResult(mappedSponsorship);

        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
