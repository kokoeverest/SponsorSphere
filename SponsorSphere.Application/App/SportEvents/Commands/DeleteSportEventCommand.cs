﻿using MediatR;
using Microsoft.Extensions.Logging;
using SponsorSphere.Application.Interfaces;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record DeleteSportEventCommand(int SportEventId) : IRequest;
public class DeleteSportEventCommandHandler : IRequestHandler<DeleteSportEventCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteSportEventCommandHandler> _logger;

    public DeleteSportEventCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteSportEventCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(DeleteSportEventCommand request, CancellationToken cancellationToken)
    {
        var start = DateTime.Now;
        _logger.LogInformation("Action: {Action}", request.ToString());

        try
        {
            await _unitOfWork.BeginTransactionAsync();
            await _unitOfWork.SportEventsRepository.DeleteAsync(request.SportEventId);
            await _unitOfWork.CommitTransactionAsync();
            _logger.LogInformation("Action: {Action}, ({DT})ms", request.ToString(), (DateTime.Now - start).TotalMilliseconds);
        }

        catch (Exception) 
        {
            await _unitOfWork.RollbackTransactionAsync();
            _logger.LogError("Action: {Action} failed", request.ToString());
            throw;
        }
    } 
        
}

