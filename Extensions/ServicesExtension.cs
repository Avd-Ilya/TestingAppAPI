using TestingAppApi.Auth;
using TestingAppApi.Users;

namespace TestingAppApi.Extensions;

public static class ServicesExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITestingAppRepository, TestingAppRepository>();
        services.AddSingleton<IJwtService, JwtService>();
        return services;
    }
}