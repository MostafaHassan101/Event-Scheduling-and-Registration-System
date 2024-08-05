using EventSystem.Application.Common.Interfaces;
using System.Security.Claims;

namespace API.Services;
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    public int DomainUserId => TryGetDomainUserId();

    int TryGetDomainUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

        if(int.TryParse(userId, out var domainUserId))
             return domainUserId;

        throw new UnauthorizedAccessException();
    }
}