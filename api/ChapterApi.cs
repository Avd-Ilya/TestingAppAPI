public class ChapterApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/chapters", async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetChaptersAsync()))
            .WithTags("Chapters");

        app.MapGet("/chapters/{id}", async (int id, ITestingAppRepository repository) =>
            await repository.GetChapterAsync(id) is Chapter chapter
            ? Results.Ok(chapter)
            : Results.NotFound())
            .WithTags("Chapters");

        app.MapPost("/chapters", async ([FromBody] Chapter chapter, ITestingAppRepository repository) =>
        {
            await repository.InsertChapterAsync(chapter);
            await repository.SaveAsync();
            return Results.Created($"/chapters/{chapter.Id}", chapter);
        })
            .WithTags("Chapters");

        app.MapPut("/chapters", async ([FromBody] Chapter chapter, ITestingAppRepository repository) =>
        {
            await repository.UpdateChapterAsync(chapter);
            await repository.SaveAsync();
            return Results.Created($"/chapters/{chapter.Id}", chapter);
        })
            .WithTags("Chapters");

        app.MapDelete("/chapters/{id}", async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteChapterAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("Chapters");
    }
}