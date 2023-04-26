using Microsoft.AspNetCore.Authorization;

public class TestApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/tests", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetTestsAsync()))
            .WithTags("Tests");

        app.MapGet("/tests/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
            await repository.GetTestAsync(id) is Test test
            ? Results.Ok(test)
            : Results.NotFound())
            .WithTags("Tests");

        app.MapPost("/tests", [Authorize] async ([FromBody] Test test, ITestingAppRepository repository) =>
        {
            await repository.InsertTestAsync(test);
            await repository.SaveAsync();
            return Results.Created($"/tests/{test.Id}", test);
        })
            .WithTags("Tests");

        app.MapPut("/tests", [Authorize] async ([FromBody] Test test, ITestingAppRepository repository) =>
        {
            await repository.UpdateTestAsync(test);
            await repository.SaveAsync();
            return Results.Created($"/tests/{test.Id}", test);
        })
            .WithTags("Tests");

        app.MapDelete("/tests/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteTestAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("Tests");
    }
}