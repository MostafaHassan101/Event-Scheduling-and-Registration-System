using AutoMapper;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using MediatR;

namespace EventSystem.Application.EventManagement.Queries.GetEventById;

public record GetEventByIdQuery : IRequest<Event>
{
    public int EventId { get; set; }
}

public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, Event>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public GetEventByIdQueryHandler(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<Event> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
	{
        return await _eventRepository.GetByIdAsync(request.EventId);
	}
}