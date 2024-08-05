using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using MediatR;

namespace EventSystem.Application.EventRegistration.Queries.GetAllEventsForUser;


public record GetAllEventsForUserQuery : IRequest<IEnumerable<Event>>
{
	public int UserId { get; set; }
}

public class GetAllEventsForUserQueryHandler : IRequestHandler<GetAllEventsForUserQuery, IEnumerable<Event>>
{
	private readonly IEventRepository _eventRepository;

    public GetAllEventsForUserQueryHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<IEnumerable<Event>> Handle(GetAllEventsForUserQuery request, CancellationToken cancellationToken)
	{
        return await _eventRepository.GetAllEventsByUserIdAsync(request.UserId);

    }
}
