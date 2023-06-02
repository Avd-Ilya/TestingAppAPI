using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

public class TrackedTestApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/tracked-tests", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetTrackedTestsAsync()))
            .WithTags("TrackedTests");

        app.MapGet("/my-tracked-tests", [Authorize] async (HttpContext http, ITestingAppRepository repository) =>
        {
            var token = await http.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            
            return Results.Ok(await repository.GetTrackedTestsForUserAsync(userId));
        })  
            .WithTags("TrackedTests");


        app.MapGet("/activity-tracked-tests/{id}", [Authorize] async (int id, HttpContext http, ITestingAppRepository repository) =>
        {
            var passedTests = await repository.GetPassedTestsByTrackedTestIdAsync(id);
            var response = new List<ActivityTrackedTestResponse>();
            foreach (var passedTest in passedTests)
            {
                var action = new ActivityTrackedTestResponse();
                action.PassedTest = passedTest;
                action.User = await repository.GetUserAsync(passedTest.UserId.ToString());
                response.Add(action);
            }
            return Results.Ok(response);
        })  
            .WithTags("TrackedTests");

        app.MapGet("/tracked-tests/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
            await repository.GetTrackedTestAsync(id) is TrackedTest trackedTest
            ? Results.Ok(trackedTest)
            : Results.NotFound())
            .WithTags("TrackedTests");

        app.MapGet("/tracked-tests/key/{key}", [Authorize] async (String key, ITestingAppRepository repository) =>
            await repository.GetTrackedTestByKeyAsync(key) is TrackedTest trackedTest
            ? Results.Ok(trackedTest)
            : Results.NotFound())
            .WithTags("TrackedTests");

        app.MapPost("/tracked-tests", [Authorize] async ([FromBody] TrackedTest trackedTest, HttpContext http, ITestingAppRepository repository) =>
        {
            var token = await http.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            trackedTest.UserId = Guid.Parse(userId);

            Random random = new Random();
            string key = random.Next(10000000, 99999999).ToString();
            var trackedTestWithKey = await repository.GetTrackedTestByKeyAsync(key);
            if (trackedTestWithKey != null)
            {
                return Results.Problem(detail: "Key already is used");
            }
            trackedTest.Key = key;

            await repository.InsertTrackedTestAsync(trackedTest);
            await repository.SaveAsync();
            return Results.Created($"/tracked-tests/{trackedTest.Id}", trackedTest);
        })
            .WithTags("TrackedTests");

        app.MapPut("/tracked-tests", [Authorize] async ([FromBody] TrackedTest trackedTest, ITestingAppRepository repository) =>
        {
            await repository.UpdateTrackedTestAsync(trackedTest);
            await repository.SaveAsync();
            return Results.Created($"/tests/{trackedTest.Id}", trackedTest);
        })
            .WithTags("TrackedTests");

        app.MapDelete("/tracked-tests/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteTrackedTestAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("TrackedTests");
    }
}