using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.App.SponsorCompanies.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorCompanies.Commands;

public record UpdateSponsorCompanyCommand(UpdateSponsorCompanyDto SponsorCompanyToUpdate) : IRequest<SponsorCompanyDto>;

public class UpdateSponsorCompanyCommandHandler : IRequestHandler<UpdateSponsorCompanyCommand, SponsorCompanyDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdateSponsorCompanyCommandHandler> _logger;
    private readonly IMapper _mapper;

    public UpdateSponsorCompanyCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateSponsorCompanyCommandHandler> logger, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<SponsorCompanyDto> Handle(UpdateSponsorCompanyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var start = DateTime.Now;
            _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

            await _unitOfWork.BeginTransactionAsync();

            var loggedUser = await _userManager.FindByEmailAsync(request.SponsorCompanyToUpdate.Email);
            Picture? existingPicture;

            if (loggedUser is null)
            {
                throw new NotFoundException("User is not found!");
            }

            if (loggedUser.PictureId == 0)
            {
                existingPicture = null;
            }
            else
            {
                existingPicture = await _unitOfWork.PicturesRepository.GetByIdAsync(loggedUser.PictureId);
            }

            var newProfilePicture = request.SponsorCompanyToUpdate.PictureId is not null
                ? await PictureHelper.TransformFileToPicture(request.SponsorCompanyToUpdate.PictureId, cancellationToken)
                : null;

            var updatedSponsorCompany = _mapper.Map<SponsorCompanyDto>(request.SponsorCompanyToUpdate);

            if (existingPicture != null && newProfilePicture != null)
            {
                newProfilePicture.Id = existingPicture.Id;

                var mappedPicture = _mapper.Map<PictureDto>(newProfilePicture);

                await _unitOfWork.PicturesRepository.UpdateAsync(mappedPicture);
                updatedSponsorCompany.PictureId = newProfilePicture.Id;
            }

            else if (newProfilePicture != null)
            {
                newProfilePicture = await _unitOfWork.PicturesRepository.CreateAsync(newProfilePicture);
                updatedSponsorCompany.PictureId = newProfilePicture.Id;
            }

            else if (existingPicture != null)
            {
                updatedSponsorCompany.PictureId = existingPicture.Id;
            }

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