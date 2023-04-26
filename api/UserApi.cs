using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using TestingAppApi.Extensions;
using TestingAppApi.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

public class UserApi
{
    public void Register(WebApplication app)
    {
        app.MapPost("/register", async (User user, ITestingAppRepository repository) =>
        {
            var token = await repository.Register(user);
            return Results.Ok(token);
        })  
            .WithValidator<User>()
            .WithTags("Users");
            
        app.MapPost("/login", async (User user, ITestingAppRepository repository) =>
        {
            var token = await repository.Login(user);
            return Results.Ok(token);
        })  
            .WithValidator<User>()
            .WithTags("Users");   

        app.MapGet("/users", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetUsersAsync()))
            .WithTags("Users");      

        app.MapGet("/user-info", [HttpGet] [Authorize] async (HttpContext http, ITestingAppRepository repository) =>
        {
            var token = await http.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            // Расшифровка токена
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);

            // Получение идентификатора пользователя из полезной нагрузки
            var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            return await repository.GetUserAsync(userId ?? "") is User user
            ? Results.Ok(user)
            : Results.NotFound();
        })
            .WithTags("Users");      

        app.MapPut("/users", async ([FromBody] User user, HttpContext http, ITestingAppRepository repository) =>
        {
            var token = await http.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            // Расшифровка токена
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);

            // Получение идентификатора пользователя из полезной нагрузки
            var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            user.Id = Guid.Parse(userId);
            var success = await repository.UpdateUserAsync(user);
            await repository.SaveAsync();
            return success is true 
            ? Results.Created($"/users/{user.Id}", user)
            : Results.NotFound();
            // return Results.Created($"/users/{user.Id}", user);
        })
            .WithTags("Users");

        app.MapDelete("/users/{id}", async (String id, ITestingAppRepository repository) =>
        {
            var success = await repository.DeleteUserAsync(id);
            await repository.SaveAsync();
            return success is true 
            ? Results.Ok()
            : Results.NotFound();
            // return Results.NoContent();
        })
            .WithTags("Users");
    }
}