using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record UpdateSponsorIndividualCommand(
    SponsorIndividualDto SponsorIndividualToUpdate) : IRequest<SponsorIndividualDto>;

public class UpdateSponsorIndividualCommandHandler : IRequestHandler<UpdateSponsorIndividualCommand, SponsorIndividualDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSponsorIndividualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SponsorIndividualDto> Handle(UpdateSponsorIndividualCommand request, CancellationToken cancellationToken)
    {
        var updatedSponsorIndividual = _unitOfWork.SponsorIndividualsRepository.UpdateAsync(request.SponsorIndividualToUpdate);

        var updatedSponsorCompanyDto = _mapper.Map<SponsorIndividualDto>(updatedSponsorIndividual);

        return await Task.FromResult(updatedSponsorCompanyDto);
    }
}