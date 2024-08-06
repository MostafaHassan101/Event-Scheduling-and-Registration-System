using EventSystem.Application.Common.Interfaces;
using EventSystem.Application.Common.Models;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using MediatR;

namespace EventSystem.Application.EventRegistration.Queries.GetAllEventsForUser;


public record GetAllEventsForUserQuery : IRequest<IEnumerable<Event>>
{
}

public class GetAllEventsForUserQueryHandler : IRequestHandler<GetAllEventsForUserQuery, IEnumerable<Event>>
{
	private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;
    public GetAllEventsForUserQueryHandler(IEventRepository eventRepository, IUserRepository userRepository, ICurrentUserService currentUserService)
    {
        _eventRepository = eventRepository;
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<Event>> Handle(GetAllEventsForUserQuery request, CancellationToken cancellationToken)
	{
        var username = _currentUserService.UserName;
        var user = await _userRepository.GetUserByEmailAsync(username);

        var result = await _eventRepository.GetAllEventsByUserIdAsync(user.Id);
        
        return result;
    }
}
