using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record UpdateSponsorIndividualCommand(
    UpdateSponsorIndividualDto SponsorIndividualToUpdate) : IRequest<SponsorIndividualDto>;

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
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            var transformedPicture = request.SponsorIndividualToUpdate.PictureId is not null
                    ? await PictureHelper.TransformFileToPicture(request.SponsorIndividualToUpdate.PictureId, cancellationToken)
                    : null;

            if (transformedPicture != null)
            {
                transformedPicture = await _unitOfWork.PicturesRepository.CreateAsync(transformedPicture);
            }

            var updatedSponsorCompany = _mapper.Map<SponsorIndividualDto>(request.SponsorIndividualToUpdate);
            updatedSponsorCompany.PictureId = transformedPicture?.Id ?? 0;


            var result = await _unitOfWork.SponsorIndividualsRepository.UpdateAsync(updatedSponsorCompany);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return result;
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}