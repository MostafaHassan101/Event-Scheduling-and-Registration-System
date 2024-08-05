using EventSystem.Application.Common.Interfaces;
using EventSystem.Domain.Repositories;
using EventSystem.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace EventSystem.Infrastructure.Services;

public class EventRegistrationService : IEventRegistrationService
{
    private readonly IEventRepository _eventRepository;
    private readonly IUserRepository _userRepository;

    public EventRegistrationService(IEventRepository eventRepository, IUserRepository userRepository)
    {
        _eventRepository = eventRepository;
        _userRepository = userRepository;
    }

    public async Task RegisterUserForEventAsync(int userId, int eventId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("User not found", nameof(userId));

        var @event = await _eventRepository.GetByIdAsync(eventId);
        if (@event == null)
            throw new ArgumentException("Event not found", nameof(eventId));

        user.AddEvent(@event);

        await _userRepository.UpdateAsync(user);
        await _eventRepository.UpdateAsync(@event);
    }

    public async Task CancelUserRegistrationAsync(int userId, int eventId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new ArgumentException("User not found", nameof(userId));

        var @event = await _eventRepository.GetByIdAsync(eventId);
        if (@event == null)
            throw new ArgumentException("Event not found", nameof(eventId));

        user.RemoveEvent(@event);

        await _userRepository.UpdateAsync(user);
        await _eventRepository.UpdateAsync(@event);
    }

    //public class UserEventService
    //{
    //    private readonly UserManager<ApplicationUser> _userManager;
    //    private readonly IEventRepository _eventRepository;
    //    private readonly IUserRepository _userRepository;

    //    public UserEventService(UserManager<ApplicationUser> userManager,
    //                            IEventRepository eventRepository,
    //                            IUserRepository userRepository)
    //    {
    //        _userManager = userManager;
    //        _eventRepository = eventRepository;
    //        _userRepository = userRepository;
    //    }

    //    public async Task AddEventToUserAsync(int userId, int eventId)
    //    {
    //        var appUser = await _userManager.FindByIdAsync(userId.ToString());
    //        if (appUser == null)
    //            throw new ArgumentException("User not found", nameof(userId));

    //        var user = await _userRepository.GetByIdAsync(appUser.UserId);

    //        var @event = await _eventRepository.GetByIdAsync(eventId);

    //        user.AddEvent(@event);
    //        await _userRepository.UpdateAsync(user);
    //        await _eventRepository.UpdateAsync(@event);

    //        //appUser.UpdateFromUser(user);
    //        //await _userManager.UpdateAsync(appUser);
    //    }

    //    public async Task RemoveEventFromUserAsync(int userId, int eventId)
    //    {
    //        var appUser = await _userManager.FindByIdAsync(userId.ToString());
    //        if (appUser == null)
    //            throw new ArgumentException("User not found", nameof(userId));

    //        var user = await _userRepository.GetByIdAsync(appUser.UserId);

    //        var @event = await _eventRepository.GetByIdAsync(eventId);

    //        user.RemoveEvent(@event);
    //        await _userRepository.UpdateAsync(user);
    //        await _eventRepository.UpdateAsync(@event);

    //        //appUser.UpdateFromUser(user);
    //        //await _userManager.UpdateAsync(appUser);
    //    }

    //    //public async Task UpdateUserEventAsync(int userId, Event updatedEvent)
    //    //{
    //    //    var appUser = await _userManager.FindByIdAsync(userId.ToString());
    //    //    if (appUser == null)
    //    //        throw new ArgumentException("User not found", nameof(userId));

    //    //    var user = await _userRepository.GetByIdAsync(appUser.UserId);
    //    //    if (user == null)
    //    //        throw new ArgumentException("Domain User not found", nameof(userId));

    //    //    user.UpdateEvent(updatedEvent);
    //    //    await _userRepository.UpdateAsync(user);
    //    //    await _eventRepository.UpdateAsync(updatedEvent);

    //    //    appUser.UpdateFromUser(user);
    //    //    await _userManager.UpdateAsync(appUser);
    //    //}
    //}
}