using TestingAppApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TestingAppApi.Extensions;

public static class DatabaseExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<TestingAppDb>(options => 
            options.UseSqlite("Data source=IlyaDatabase.db"));

        return services;
    }

    public static WebApplication DatabaseEnsureCreated(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TestingAppDb>();
        context.Database.EnsureCreated();
        // context.Database.EnsureDeleted();
        return app;
    }
}
