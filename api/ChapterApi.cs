using Microsoft.AspNetCore.Authorization;

public class ChapterApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/chapters", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetChaptersAsync()))
            .WithTags("Chapters");

        app.MapGet("/chapters/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
            await repository.GetChapterAsync(id) is Chapter chapter
            ? Results.Ok(chapter)
            : Results.NotFound())
            .WithTags("Chapters");

        app.MapPost("/chapters", [Authorize] async ([FromBody] Chapter chapter, ITestingAppRepository repository) =>
        {
            await repository.InsertChapterAsync(chapter);
            await repository.SaveAsync();
            return Results.Created($"/chapters/{chapter.Id}", chapter);
        })
            .WithTags("Chapters");

        app.MapPut("/chapters", [Authorize] async ([FromBody] Chapter chapter, ITestingAppRepository repository) =>
        {
            await repository.UpdateChapterAsync(chapter);
            await repository.SaveAsync();
            return Results.Created($"/chapters/{chapter.Id}", chapter);
        })
            .WithTags("Chapters");

        app.MapDelete("/chapters/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteChapterAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("Chapters");
    }
}