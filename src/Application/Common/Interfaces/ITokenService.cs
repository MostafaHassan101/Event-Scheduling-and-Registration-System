namespace EventSystem.Infrastructure.Services
{
    public interface ITokenService<TIdentityUer> where TIdentityUer : class
    {
        string GenerateToken(TIdentityUer identityUer);
    }
}