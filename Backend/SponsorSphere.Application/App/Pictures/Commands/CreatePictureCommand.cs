using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Goals.Dtos;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Common.Exceptions;
using SponsorSphere.Application.Common.Helpers;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.Pictures.Commands;

public record CreatePictureCommand(CreatePictureDto Picture) : IRequest<PictureDto>;
public class CreatePictureCommandHandler : IRequestHandler<CreatePictureCommand, PictureDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<CreatePictureCommandHandler> _logger;
    public CreatePictureCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<CreatePictureCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<PictureDto> Handle(CreatePictureCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        var picture = await PictureHelper.TransformFileToPicture(request.Picture.FormFile, cancellationToken);

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.PicturesRepository.CreateAsync(picture);
            await _unitOfWork.CommitTransactionAsync();

            var mappedPicture = _mapper.Map<PictureDto>(picture);

            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return await Task.FromResult(mappedPicture);
        }
        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }

    }
}