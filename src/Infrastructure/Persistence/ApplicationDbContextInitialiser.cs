using EventSystem.Application.Common.Interfaces;
using EventSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EventSystem.Application.UserMangment.UserRoles;
using EventSystem.Domain.Repositories;
using EventSystem.Domain.Entities;
using EventSystem.Domain.ValueObjects;
using EventSystem.Infrastructure.Services;

namespace EventSystem.Infrastructure.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IIdentityService _identityService;
    private IUserRegistrationService _userRegistrationService;
    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, IEventRepository eventRepository
        , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IIdentityService identityService, ApplicationDbContext context, IUserRegistrationService userRegistrationService, IUserRepository userRepository)
    {
        _logger = logger;
        _eventRepository = eventRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _identityService = identityService;
        _context = context;
        _userRegistrationService = userRegistrationService;
        _userRepository = userRepository;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            //if (_context.Database.IsSqlServer())
            //{
            //    await _context.Database.MigrateAsync();
            //}
            if (_context.Database.IsNpgsql())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        /// Default Roles
        await SeedRolesAsync();

        /// Default users
        await SeedUsersAsync();

        /// Default Events
        await SeedEventsAsync();

    }
    private async Task SeedRolesAsync()
    {
        if (!_context.Roles.Any())
        {
            var roles = new List<IdentityRole> { new IdentityRole(SysRoles.Admin), new IdentityRole(SysRoles.User) };
            await _context.AddRangeAsync(roles);
        }
    }

    private async Task SeedUsersAsync()
    {
        if (! _context.Users.Any())
        {
            ContactInformation contact = new ContactInformation("01234567891");
            var defaultUser = new User("Default User", "user@default.com", contact);
            await _userRegistrationService.RegisterUserAsync(defaultUser, "User@12345", "User@12345");
        }
    }

    private async Task SeedEventsAsync()
    {
        var events = new List<Event>
        {
            new Event(
                "Annual Science Conference",
                "A conference to discuss the latest in science research.",
                new EventDate(new DateTime(2024, 9, 20)),
                new EventTime(new TimeSpan(9, 0, 0)),
                new EventLocation( "123 Science Rd.", "Science City", "Egypt", "12345")
            ),
            new Event(
                "AI Symposium",
                "A symposium on advancements in artificial intelligence.",
                new EventDate(new DateTime(2024, 10, 15)),
                new EventTime(new TimeSpan(10, 0, 0)),
                new EventLocation("456 Technology Ave.", "Innovation City", "USA", "67890")
            ),
            new Event(
                "Health Research Summit",
                "Summit focused on the latest health research findings.",
                new EventDate(new DateTime(2024, 11, 5)),
                new EventTime(new TimeSpan(8, 30, 0)),
                new EventLocation("789 Wellness Blvd.", "Healthy Town", "UK", "54321")
            )
        };
        if (!_context.Events.Any())
        {
            await _eventRepository.AddBulk(events);
        }
    }
}
