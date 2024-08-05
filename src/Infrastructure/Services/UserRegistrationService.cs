using EventSystem.Application.Common.Exceptions;
using EventSystem.Application.Common.Interfaces;
using EventSystem.Domain.Entities;
using EventSystem.Domain.Repositories;

namespace EventSystem.Infrastructure.Services;

public class UserRegistrationService : IUserRegistrationService
{
    private readonly IUserRepository _userRepository;
    private readonly IIdentityService _identityService;

    public UserRegistrationService(IUserRepository userRepository, IIdentityService identityService)
    {
        _userRepository = userRepository;
        _identityService = identityService;
    }

    public async Task RegisterUserAsync(User user, string password, string confirmPass)
    {
        try
        {
            await _userRepository.AddAsync(user);
            await _identityService.CreateUserAsync(user.Email, user.Name, user.ContactInformation.Value, password, confirmPass);
        }
        catch (Exception ex)
        {
            throw new BadRequestException("Faild to register user");
        }
    }

    public async Task<string> LoginUserAsync(string email, string password, bool rememberMe = false)
    {
        var result = await _identityService.LoginUserAndGetTokenAsync(email, password, rememberMe);
        if (!result.Item1.Succeeded || result.Item2 == null) return null;

        return result.Item2;
    }
}