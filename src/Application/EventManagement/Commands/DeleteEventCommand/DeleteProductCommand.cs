using EventSystem.Domain.Repositories;
using MediatR;

namespace EventSystem.Application.EventManagement.Commands.DeleteEventCommand;

public record DeleteEventCommand : IRequest
{
    public int Id { get; init; }
}

public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }
    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
    }
}
