using EventSystem.Application.Common.Interfaces;
using MediatR;

namespace EventSystem.Application.EventRegistration.Commands.RegisterUserToEvent;

public record RegisterUserToEventCommand : IRequest
{
    public int EventId { get; set; }
}

public class RegisterUserToEventCommandHandler : IRequestHandler<RegisterUserToEventCommand>
{
    private readonly IEventRegistrationService _eventRegistrationService;
    private readonly ICurrentUserService _currentUserService;
    public RegisterUserToEventCommandHandler(IEventRegistrationService eventRegistrationService, ICurrentUserService currentUserService)
    {
        _eventRegistrationService = eventRegistrationService;
        _currentUserService = currentUserService;
    }

    public async Task Handle(RegisterUserToEventCommand request, CancellationToken cancellationToken)
	{
        var userId = _currentUserService.DomainUserId;
        await _eventRegistrationService.RegisterUserForEventAsync(userId, request.EventId);
	}
}