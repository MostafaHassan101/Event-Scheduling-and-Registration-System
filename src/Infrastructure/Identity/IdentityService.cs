using AutoMapper;
using EventSystem.Application.Common.Interfaces;
using EventSystem.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EventSystem.Application.UserMangment.UserRoles;
using EventSystem.Domain.Entities;
using EventSystem.Domain.ValueObjects;
using System.Text.RegularExpressions;
using EventSystem.Infrastructure.Services;
using EventSystem.Application.Common.Exceptions;

namespace EventSystem.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly IAuthorizationService _authorizationService;
    private readonly IMapper _mapper;
    private readonly ITokenService<ApplicationUser> _tokenService;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IMapper mapper,
        ITokenService<ApplicationUser> tokenService,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _mapper = mapper;
        _tokenService = tokenService;
        _roleManager = roleManager;
    }

    private async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<string?> GetUserNameAsync(string userName)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userName);

        return user?.UserName;
    }
    public async Task<string?> GetUserIdAsync(string userName)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Email == userName);

        return user?.Id.ToString();
    }


    public async Task<(Result Result, string UserId)> CreateUserAsync(User domainUser, string password, string confirmPassword)
    {
        var user = new ApplicationUser(domainUser);

        var result = await _userManager.CreateAsync(user, CheckPasswordValidation(password, confirmPassword));

        if (result.Succeeded)
        {
            if (!await _roleManager.RoleExistsAsync(SysRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(SysRoles.User));
            await _userManager.AddToRoleAsync(user, SysRoles.User);

        }

        return (result.ToApplicationResult(), user.Id.ToString());
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync<T>(T applicationUserDto, string password)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(applicationUserDto);

        var result = await _userManager.CreateAsync(applicationUser, password);
        if (result.Succeeded)
        {
            // Assign a role to the user (if needed)
            await _userManager.AddToRoleAsync(applicationUser, SysRoles.User);

        }

        return (result.ToApplicationResult(), applicationUser.Id.ToString());
    }

    public async Task<Result> UpdateUserAsync<T>(string userId, T updateUserDto)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id.ToString() == userId);

        if (user == null)
            return Result.Failure(new List<string> { "User not found" });

        var updateUser = _mapper.Map(updateUserDto, user);

        var result = await _userManager.UpdateAsync(updateUser);

        return result.ToApplicationResult();
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id.ToString() == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id.ToString() == userId);
        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id.ToString() == userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<bool> UserIsExist(string email)
    {
        return await _userManager.Users.AnyAsync(u => u.Email == email);
    }

    public async Task SignOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<Result> SignInAsync(string email, string password, bool rememberMe)
    {
        //var sign = _signInManager.PasswordSignInAsync(email, password, rememberMe, false).Result;

        var sign = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        return sign.Succeeded ? Result.Success() : Result.Failure(new List<string> { "Invalid login attempt." });
    }

    public async Task<(Result, string?)> LoginUserAndGetTokenAsync(string email, string password, bool rememberMe)
    {
        var errors = new List<string>();

        var signUser = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
        if (!signUser.Succeeded)
        {
            errors.Add("faild to login invalid email or password");
            return (Result.Failure(errors), null);
        }

        var user = await GetUserByEmailAsync(email);
        if (user is null)
            throw new NotFoundException("user not found");

        return (Result.Success(), _tokenService.GenerateToken(user));
    }

    public async Task<List<string>> GetUserRolesAsync(string userId)
    {
        var user = await _userManager.Users.SingleAsync(u => u.Id.ToString() == userId);
        var roles = await _userManager.GetRolesAsync(user);
        return user != null ? (await _userManager.GetRolesAsync(user)).ToList() : new List<string>();
    }

    // change password
    public async Task<Result> ChangePasswordAsync(string userId, string currentPassword, string newPassword)
    {
        var user = await _userManager.Users.SingleAsync(u => u.Id.ToString() == userId);
        var validPassword = await _userManager.CheckPasswordAsync(user, currentPassword);
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.ToApplicationResult();
    }

    private string CheckPasswordValidation(string password, string confirmPassword)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
            throw new ArgumentException("invalide password or confirmPassword");

        if (password.Length < 8 || confirmPassword.Length < 8)
            throw new ArgumentException("password length at least 8 characters");

        if (!string.Equals(password, confirmPassword))
            throw new ArgumentException("password and confirmPassword isn't match");

        //if (!AppConstants.PasswordPattern.IsMatch(password))      // if needed
        //    throw new ArgumentException("weak password");

        return password;
    }
}