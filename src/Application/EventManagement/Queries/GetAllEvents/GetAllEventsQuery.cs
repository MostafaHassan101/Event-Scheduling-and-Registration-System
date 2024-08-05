using AutoMapper;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using MediatR;

namespace EventSystem.Application.EventManagement.Queries.GetEventsQuery;

public record GetAllEventsQuery : IRequest<IEnumerable<Event>>
{
}

public class QueryHandler : IRequestHandler<GetAllEventsQuery, IEnumerable<Event>>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    public QueryHandler(IMapper mapper, IEventRepository eventRepository)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
    }
    public async Task<IEnumerable<Event>> Handle(GetAllEventsQuery request, CancellationToken cancellationToken)
    {
        return await _eventRepository.GetAllAsync();

    }
}
