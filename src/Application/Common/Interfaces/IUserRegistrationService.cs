using EventSystem.Domain.Entities;

namespace EventSystem.Infrastructure.Services
{
    public interface IUserRegistrationService
    {
        Task<string> LoginUserAsync(string email, string password, bool rememberMe = false);
        Task RegisterUserAsync(User user, string password, string confirmPass);
    }
}