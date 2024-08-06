using EventSystem.Application.Common.Interfaces;
using EventSystem.Domain.Repositories;
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
    private readonly IUserRepository _userRepository;
    public RegisterUserToEventCommandHandler(IEventRegistrationService eventRegistrationService, ICurrentUserService currentUserService, IUserRepository userRepository)
    {
        _eventRegistrationService = eventRegistrationService;
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public async Task Handle(RegisterUserToEventCommand request, CancellationToken cancellationToken)
	{
        var username = _currentUserService.UserName;
        var user = await _userRepository.GetUserByEmailAsync(username);
        await _eventRegistrationService.RegisterUserForEventAsync(user.Id, request.EventId);
    }
}