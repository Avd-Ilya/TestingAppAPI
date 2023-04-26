using TestingAppApi.Users;

namespace TestingAppApi.Auth;

public interface IJwtService
{
    public string GenerateToken(User user);
}