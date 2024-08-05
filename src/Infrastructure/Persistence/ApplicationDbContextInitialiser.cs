using EventSystem.Application.Common.Interfaces;
using EventSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EventSystem.Application.UserMangment.UserRoles;
using EventSystem.Domain.Repositories;

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

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, IEventRepository eventRepository
        , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IIdentityService identityService, IUserRepository userRepository, ApplicationDbContext context)
    {
        _logger = logger;
        _eventRepository = eventRepository;
        _userManager = userManager;
        _roleManager = roleManager;
        _identityService = identityService;
        _userRepository = userRepository;
        _context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
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
        /// Default users
        await SeedUsersAsync();

        /// Default Events
        await SeedEventsAsync();

    }

    private async Task SeedUsersAsync()
    {
       
    }

    private async Task SeedEventsAsync()
    {
        
    }
}
