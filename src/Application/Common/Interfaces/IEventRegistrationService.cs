namespace EventSystem.Application.Common.Interfaces
{
    public interface IEventRegistrationService
    {
        Task CancelUserRegistrationAsync(int userId, int eventId);
        Task RegisterUserForEventAsync(int userId, int eventId);
    }
}