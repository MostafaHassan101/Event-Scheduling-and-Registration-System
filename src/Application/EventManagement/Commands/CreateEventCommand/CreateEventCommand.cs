using AutoMapper;
using EventSystem.Application.Common.Mappings;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using EventSystem.Domain.ValueObjects;
using MediatR;

namespace EventSystem.Application.EventManagement.Commands.CreateEventCommand;

public record CreateEventCommand : IRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan Time { get; set; }
    public string Address { get; private set; }
    public string City { get; private set; }
    public string Country { get; private set; }
    public string PostalCode { get; private set; }
    private readonly List<User> _participants = new List<User>();
}

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
    }
    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var date = new EventDate(request.Date);
        var time = new EventTime(request.Time);
        var location = new EventLocation(request.Address, request.City, request.Country, request.PostalCode);

        var @event = new Event(request.Title, request.Description, date, time, location);
        await _eventRepository.AddAsync(@event);
    }
}