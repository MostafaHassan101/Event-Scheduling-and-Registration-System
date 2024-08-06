using EventSystem.Application.Common.Interfaces;
using EventSystem.Domain.Repositories;
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
    private readonly IUserRepository _userRepository;

    public CancelUserRegistrationFromEventCommandHandler(IEventRegistrationService eventRegistrationService, ICurrentUserService currentUserService, IUserRepository userRepository)
    {
        _eventRegistrationService = eventRegistrationService;
        _currentUserService = currentUserService;
        _userRepository = userRepository;
    }

    public async Task Handle(CancelUserRegistrationFromEventCommand request, CancellationToken cancellationToken)
    {
        var username = _currentUserService.UserName;
        var user = await _userRepository.GetUserByEmailAsync(username);

        await _eventRegistrationService.CancelUserRegistrationAsync(user.Id, request.EventId);
    }
}