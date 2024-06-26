﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Athletes.Dtos;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Athletes.Commands;

public record UpdateAthleteCommand(UpdateAthleteDto AthleteToUpdate) : IRequest<AthleteDto>;

public class UpdateAthleteCommandHandler : IRequestHandler<UpdateAthleteCommand, AthleteDto>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateAthleteCommandHandler> _logger;

    public UpdateAthleteCommandHandler(ILogger<UpdateAthleteCommandHandler> logger, UserManager<User> userManager, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
        _logger = logger;
    }

    public async Task<AthleteDto> Handle(UpdateAthleteCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var start = DateTime.Now;
            _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

            await _unitOfWork.BeginTransactionAsync();

            var loggedUser = await _userManager.FindByEmailAsync(request.AthleteToUpdate.Email);
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

            var newProfilePicture = request.AthleteToUpdate.PictureId is not null
                ? await PictureHelper.TransformFileToPicture(request.AthleteToUpdate.PictureId, cancellationToken)
                : null;

            var updatedAthlete = _mapper.Map<AthleteDto>(request.AthleteToUpdate);

            if (existingPicture != null && newProfilePicture != null)
            {
                newProfilePicture.Id = existingPicture.Id;

                var mappedPicture = _mapper.Map<PictureDto>(newProfilePicture);

                await _unitOfWork.PicturesRepository.UpdateAsync(mappedPicture);
                updatedAthlete.PictureId = newProfilePicture.Id;
            }

            else if (newProfilePicture != null)
            {
                newProfilePicture = await _unitOfWork.PicturesRepository.CreateAsync(newProfilePicture);
                updatedAthlete.PictureId = newProfilePicture.Id;
            }

            else if (existingPicture != null)
            {
                updatedAthlete.PictureId = existingPicture.Id;
            }

            var result = await _unitOfWork.AthletesRepository.UpdateAsync(updatedAthlete);
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
