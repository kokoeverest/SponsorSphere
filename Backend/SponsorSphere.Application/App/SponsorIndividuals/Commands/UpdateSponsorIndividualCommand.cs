using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.App.SponsorIndividuals.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SponsorIndividuals.Commands;
public record UpdateSponsorIndividualCommand(
    UpdateSponsorIndividualDto SponsorIndividualToUpdate) : IRequest<SponsorIndividualDto>;

public class UpdateSponsorIndividualCommandHandler : IRequestHandler<UpdateSponsorIndividualCommand, SponsorIndividualDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSponsorIndividualCommandHandler> _logger;

    public UpdateSponsorIndividualCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UpdateSponsorIndividualCommandHandler> logger, UserManager<User> userManager)
    {
        _userManager = userManager;
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

            var loggedUser = await _userManager.FindByEmailAsync(request.SponsorIndividualToUpdate.Email);
            Picture? existingPicture;

            if (loggedUser is null)
            {
                throw new NotFoundException("User is not found!");
            }

            if (loggedUser.Picture is null)
            {
                existingPicture = null;
            }
            else
            {
                existingPicture = loggedUser.Picture;
            }

            var newProfilePicture = request.SponsorIndividualToUpdate.Picture is not null
                ? await PictureHelper.TransformFileToPicture(request.SponsorIndividualToUpdate.Picture, cancellationToken)
                : null;

            var updatedSponsorIndividual = _mapper.Map<SponsorIndividualDto>(request.SponsorIndividualToUpdate);

            if (existingPicture != null && newProfilePicture != null)
            {
                newProfilePicture.Id = existingPicture.Id;

                //var mappedPicture = _mapper.Map<PictureDto>(newProfilePicture);

                await _unitOfWork.PicturesRepository.UpdateAsync(newProfilePicture);
                updatedSponsorIndividual.Picture = newProfilePicture;
            }

            else if (newProfilePicture != null)
            {
                newProfilePicture = await _unitOfWork.PicturesRepository.CreateAsync(newProfilePicture);
                updatedSponsorIndividual.Picture = newProfilePicture;
            }

            else if (existingPicture != null)
            {
                updatedSponsorIndividual.Picture = existingPicture;
            }


            var result = await _unitOfWork.SponsorIndividualsRepository.UpdateAsync(updatedSponsorIndividual);
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