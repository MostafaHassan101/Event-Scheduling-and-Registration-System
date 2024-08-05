namespace EventSystem.Application.Common.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
    int DomainUserId { get; }
}
