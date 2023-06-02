using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using TestingAppApi.Extensions;
using TestingAppApi.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

public class UserApi
{
    public void Register(WebApplication app)
    {
        app.MapPost("/register", async (User user, ITestingAppRepository repository) =>
        {
            var userFromDB = await repository.GetUserByEmailAsync(email: user.Username);
            if (userFromDB != null)
            {
                return Results.BadRequest(error: "User already registered");
            }
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            if (user.Password.Length > 3 && regex.Match(user.Username).Success)
            {
                var token = await repository.Register(user);
                return Results.Ok(token);
            } else {
                return Results.BadRequest(error: "Incorrect data");
            }
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
        
        app.MapGet("/users/{email}", [Authorize] async (String email, ITestingAppRepository repository) =>
            Results.Ok(await repository.GetUserByEmailAsync(email)))
            .WithTags("Users");

        app.MapGet("/user-info", [HttpGet][Authorize] async (HttpContext http, ITestingAppRepository repository) =>
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
            var selectedOptions = new List<SelectedOption>();
            var userAnswers = new List<UserAnswer>();
            var passedTests = await repository.GetPassedTestsByUserAsync(id);
            foreach (var passedTest in passedTests)
            {
                userAnswers.AddRange(await repository.GetUserAnswersByPassedTestAsync(passedTest.Id));
            }
            foreach (var userAnswer in userAnswers)
            {
                selectedOptions.AddRange(await repository.GetSelectedOptionsByUserAnswerAsync(userAnswer.Id));
            }

            foreach (var selectedOption in selectedOptions)
            {
                await repository.DeleteSelectedOptionAsync(selectedOption.Id);
            }
            foreach (var userAnswer in userAnswers)
            {
                await repository.DeleteUserAnswerAsync(userAnswer.Id);
            }
            foreach (var passedTest in passedTests)
            {
                await repository.DeletePassedTestAsync(passedTest.Id);
            }

            var success = await repository.DeleteUserAsync(id);
            await repository.SaveAsync();
            return success is true
            ? Results.Ok()
            : Results.NotFound();
        })
            .WithTags("Users");
    }
}