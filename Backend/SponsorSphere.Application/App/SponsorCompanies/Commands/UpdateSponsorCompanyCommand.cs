﻿using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;

public record UpdateSponsorCompanyCommand(UpdateSponsorCompanyDto SponsorCompanyToUpdate) : IRequest<SponsorCompanyDto>;

public class UpdateSponsorCompanyCommandHandler : IRequestHandler<UpdateSponsorCompanyCommand, SponsorCompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateSponsorCompanyCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateSponsorCompanyCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateSponsorCompanyCommandHandler> logger, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<SponsorCompanyDto> Handle(UpdateSponsorCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var start = DateTime.Now;
            _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

            await _unitOfWork.BeginTransactionAsync();

            var transformedPicture = request.SponsorCompanyToUpdate.PictureId is not null
                ? await PictureHelper.TransformFileToPicture(request.SponsorCompanyToUpdate.PictureId, cancellationToken)
                : null;

            if (transformedPicture != null)
            {
                transformedPicture = await _unitOfWork.PicturesRepository.CreateAsync(transformedPicture);
            }

            var updatedSponsorCompany = _mapper.Map<SponsorCompanyDto>(request.SponsorCompanyToUpdate);
            updatedSponsorCompany.PictureId = transformedPicture?.Id ?? 0;


            var result = await _unitOfWork.SponsorCompaniesRepository.UpdateAsync(updatedSponsorCompany);
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