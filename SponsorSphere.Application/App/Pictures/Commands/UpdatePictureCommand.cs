using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Pictures.Dtos;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.Pictures.Commands;

public record UpdatePictureCommand(PictureDto PictureToUpdate) : IRequest<PictureDto>;
public class UpdatePictureCommandHandler : IRequestHandler<UpdatePictureCommand, PictureDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<UpdatePictureCommandHandler> _logger;

    public UpdatePictureCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdatePictureCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<PictureDto> Handle(UpdatePictureCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var result = await _unitOfWork.PicturesRepository.UpdateAsync(request.PictureToUpdate);
            await _unitOfWork.CommitTransactionAsync();

            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
            return result;
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError("Action: {Action} failed", request.ToString());
            throw;
        }
    }
}
