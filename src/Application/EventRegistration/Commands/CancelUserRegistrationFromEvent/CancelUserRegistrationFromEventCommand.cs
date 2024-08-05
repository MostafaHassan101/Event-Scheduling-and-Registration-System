using EventSystem.Application.Common.Interfaces;
using MediatR;

namespace EventSystem.Application.EventRegistration.Commands.CancelUserRegistrationFromEvent;


public record CancelUserRegistrationFromEventCommand : IRequest
{
    public int EventId { get; set; }
}

public class CancelUserRegistrationFromEventCommandHandler : IRequestHandler<CancelUserRegistrationFromEventCommand>
{
    private readonly IEventRegistrationService _eventRegistrationService;
    private readonly ICurrentUserService _currentUserService;

    public CancelUserRegistrationFromEventCommandHandler(IEventRegistrationService eventRegistrationService, ICurrentUserService currentUserService)
    {
        _eventRegistrationService = eventRegistrationService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(CancelUserRegistrationFromEventCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.DomainUserId;

        await _eventRegistrationService.CancelUserRegistrationAsync(userId, request.EventId);
    }
}