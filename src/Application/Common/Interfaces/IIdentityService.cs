using EventSystem.Application.Common.Models;
using System.Dynamic;

namespace EventSystem.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<(Result Result, string UserId)> CreateUserAsync<T>(T applicationUserDto, string password);
    Task<(Result Result, string UserId)> CreateUserAsync(string email, string name, string phone, string password, string confirmPassword);
    Task<string?> GetUserNameAsync(string userId);
    Task<string?> GetUserIdAsync(string userName);
    Task<bool> UserIsExist(string email);
    Task<bool> IsInRoleAsync(string userId, string role);
    Task<List<string>> GetUserRolesAsync(string userId);
    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<Result> UpdateUserAsync<T>(string userId, T applicationUserDto);


    //Task<Result> SignIn(string email);
    Task<Result> SignInAsync(string email, string password, bool rememberMe);
    Task<(Result,string?)> LoginUserAndGetTokenAsync(string email, string password, bool rememberMe);

    Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword);
    Task SignOutAsync();
    Task<Result> DeleteUserAsync(string userId);

}
