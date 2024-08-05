using EventSystem.Domain.Entities;
using EventSystem.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace EventSystem.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public int UserId { get; private set; }
    public User User { get; private set; }

    private ApplicationUser(): base() { }

    public ApplicationUser(User user) : base(user.Email)
    {
        NormalizedUserName = user.Name;
        Email = user.Email;
        UserName = user.Email;
        UserId = user.Id;
    }



}
