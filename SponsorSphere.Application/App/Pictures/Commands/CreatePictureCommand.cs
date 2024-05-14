using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.App.Pictures.Dtos;
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
        _logger.LogInformation("Action: {Action}", request.ToString());

        var picture = _mapper.Map<Picture>(request.Picture);

        await _unitOfWork.PicturesRepository.CreateAsync(picture);
        var mappedPicture = _mapper.Map<PictureDto>(picture);

        _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        return await Task.FromResult(mappedPicture);
    }
}