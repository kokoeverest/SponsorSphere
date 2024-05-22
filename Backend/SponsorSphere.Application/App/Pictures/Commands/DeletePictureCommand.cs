using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.Common.Constants;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Pictures.Commands;

public record DeletePictureCommand(PictureDto Picture) : IRequest;
public class DeletePictureCommandHandler : IRequestHandler<DeletePictureCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeletePictureCommandHandler> _logger;

    public DeletePictureCommandHandler(IUnitOfWork unitOfWork, ILogger<DeletePictureCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeletePictureCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation(LoggingConstants.logStartString, request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.PicturesRepository.DeleteAsync(request.Picture);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation(LoggingConstants.logEndString, request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}

