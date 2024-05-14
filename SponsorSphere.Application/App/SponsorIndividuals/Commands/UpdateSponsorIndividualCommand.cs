using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record UpdateSponsorIndividualCommand(
    SponsorIndividualDto SponsorIndividualToUpdate) : IRequest<SponsorIndividualDto>;

public class UpdateSponsorIndividualCommandHandler : IRequestHandler<UpdateSponsorIndividualCommand, SponsorIndividualDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSponsorIndividualCommandHandler> _logger;

    public UpdateSponsorIndividualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateSponsorIndividualCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<SponsorIndividualDto> Handle(UpdateSponsorIndividualCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        var updatedSponsorIndividual = _unitOfWork.SponsorIndividualsRepository.UpdateAsync(request.SponsorIndividualToUpdate);

        var updatedSponsorCompanyDto = _mapper.Map<SponsorIndividualDto>(updatedSponsorIndividual);

        _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        return await Task.FromResult(updatedSponsorCompanyDto);
    }
}