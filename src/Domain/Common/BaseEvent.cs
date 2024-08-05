using MediatR;

namespace EventSystem.Domain.Common;

public abstract class DomainEvent : INotification
{
    public DateTime OccurredOn { get; }

    protected DomainEvent()
    {
        OccurredOn = DateTime.UtcNow;
    }
}