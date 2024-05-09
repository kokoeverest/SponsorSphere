using AutoMapper;
using MediatR;
using SponsorSphere.Application.App.SportEvents.Dtos;
using SponsorSphere.Application.Interfaces;
using SponsorSphere.Domain.Models;

namespace SponsorSphere.Application.App.SportEvents.Commands;

public record CreateSportEventCommand(CreateSportEventDto SportEvent) : IRequest<SportEventDto>;
public class CreateSportEventCommandHandler : IRequestHandler<CreateSportEventCommand, SportEventDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CreateSportEventCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<SportEventDto> Handle(CreateSportEventCommand request, CancellationToken cancellationToken)
    {
        var sportEvent = _mapper.Map<SportEvent>(request.SportEvent);

        sportEvent.Finished = true && sportEvent.EventDate < DateTime.UtcNow.Subtract(TimeSpan.FromDays(1));
        try
        {
            await _unitOfWork.BeginTransactionAsync();
            var newSportEvent = await _unitOfWork.SportEventsRepository.CreateAsync(sportEvent);
            await _unitOfWork.CommitTransactionAsync();

            var mappedSportEvent = _mapper.Map<SportEventDto>(newSportEvent);
            return await Task.FromResult(mappedSportEvent);
        }

        catch (Exception)
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
