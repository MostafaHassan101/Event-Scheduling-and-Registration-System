using EventSystem.Application.Common.Interfaces;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using MediatR;

namespace EventSystem.Application.EventRegistration.Queries.GetAllUsersForEvent;

public record GetAllUsersForEventQuery : IRequest<IEnumerable<User>>
{
	public int EventId { get; set; }
}

public class GetAllUsersForEventQueryHandler : IRequestHandler<GetAllUsersForEventQuery, IEnumerable<User>>
{
	private readonly IEventRegistrationService _eventRegistrationService;
    private readonly IUserRepository _userRepository;

    public GetAllUsersForEventQueryHandler(IEventRegistrationService eventRegistrationService, IUserRepository userRepository)
    {
        _eventRegistrationService = eventRegistrationService;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersForEventQuery request, CancellationToken cancellationToken)
	{
        return await _userRepository.GetUsersByEventIdAsync(request.EventId);
	}
}
