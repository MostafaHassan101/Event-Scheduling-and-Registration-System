namespace EventSystem.Application.EventManagement.Commands.UpdateEventCommand;

using AutoMapper;
using EventSystem.Application.Common.Mappings;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;
using MediatR;


public record UpdateEventCommand : IRequest, IMapFrom<Event>
{
}

public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand>
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;
    public UpdateEventCommandHandler(IMapper mapper, IEventRepository eventRepository)
    {
        _mapper = mapper;
        _eventRepository = eventRepository;
    }
    public async Task Handle(UpdateEventCommand request, CancellationToken cancellationToken)
    {
    }
}
