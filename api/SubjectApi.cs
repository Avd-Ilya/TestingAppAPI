using Microsoft.AspNetCore.Authorization;

public class SubjectApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/subjects", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetSubjectsAsync()))
            .WithTags("Subjects");

        app.MapGet("/subjects/class/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
            Results.Ok(await repository.GetSubjectsByClassAsync(id)))
            .WithTags("Subjects");

        app.MapGet("/subjects/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
            await repository.GetSubjectAsync(id) is Subject subject
            ? Results.Ok(subject)
            : Results.NotFound())
            .WithTags("Subjects");

        app.MapPost("/subjects", [Authorize] async ([FromBody] Subject subject, ITestingAppRepository repository) =>
        {
            await repository.InsertSubjectAsync(subject);
            await repository.SaveAsync();
            return Results.Created($"/subjects/{subject.Id}", subject);
        })
            .WithTags("Subjects");

        app.MapPut("/subjects", [Authorize] async ([FromBody] Subject subject, ITestingAppRepository repository) =>
        {
            await repository.UpdateSubjectAsync(subject);
            await repository.SaveAsync();
            return Results.Created($"/subjects/{subject.Id}", subject);
        })
            .WithTags("Subjects");

        app.MapDelete("/subjects/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteSubjectAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("Subjects");
    }
}