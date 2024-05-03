using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorIndividuals.Responses;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Enums;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record CreateSponsorIndividualCommand(SponsorIndividual SponsorIndividual) : IRequest<SponsorIndividualDto>;

public class CreateSponsorIndividualCommandHandler : IRequestHandler<CreateSponsorIndividualCommand, SponsorIndividualDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateSponsorIndividualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorIndividualDto> Handle(CreateSponsorIndividualCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newSponsorIndividual = await _unitOfWork.SponsorIndividualsRepository.CreateAsync(request.SponsorIndividual);

            var sponsorIndividualDto = _mapper.Map<SponsorIndividualDto>(newSponsorIndividual);
            return await Task.FromResult(sponsorIndividualDto);
        }

        catch (InvalidDataException)
        {
            throw;
        }

        catch (Exception)
        {
            throw;
        }
    }
}