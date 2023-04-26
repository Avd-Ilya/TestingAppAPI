using Microsoft.AspNetCore.Authorization;

public class SelectedOptionApi {
    public void Register(WebApplication app)
    {
        app.MapGet("/selected-options", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetSelectedOptionsAsync()))
            .WithTags("SelectedOption");

        app.MapGet("/selected-options/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
            await repository.GetSelectedOptionAsync(id) is SelectedOption selectedOption
            ? Results.Ok(selectedOption)
            : Results.NotFound())
            .WithTags("SelectedOption");

        app.MapPost("/selected-options", [Authorize] async ([FromBody] SelectedOption selectedOption, ITestingAppRepository repository) =>
        {
            await repository.InsertSelectedOptionAsync(selectedOption);
            await repository.SaveAsync();
            return Results.Created($"/selected-options/{selectedOption.Id}", selectedOption);
        })
            .WithTags("SelectedOption");

        app.MapPut("/selected-options", [Authorize] async ([FromBody] SelectedOption selectedOption, ITestingAppRepository repository) =>
        {
            await repository.UpdateSelectedOptionAsync(selectedOption);
            await repository.SaveAsync();
            return Results.Created($"/selected-options/{selectedOption.Id}", selectedOption);
        })
            .WithTags("SelectedOption");

        app.MapDelete("/selected-options/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteSelectedOptionAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("SelectedOption");
    }
}