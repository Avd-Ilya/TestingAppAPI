public class SchoolClassApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/classes", async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetSchoolClassesAsync()))
            .WithTags("Classes");

        app.MapGet("/classes/{id}", async (int id, ITestingAppRepository repository) =>
            await repository.GetSchoolClassAsync(id) is SchoolClass schoolClass
            ? Results.Ok(schoolClass)
            : Results.NotFound())
            .WithTags("Classes");

        app.MapPost("/classes", async ([FromBody] SchoolClass schoolClass, ITestingAppRepository repository) =>
        {
            await repository.InsertSchoolClassAsync(schoolClass);
            await repository.SaveAsync();
            return Results.Created($"/classes/{schoolClass.Id}", schoolClass);
        })
            .WithTags("Classes");

        app.MapPut("/classes", async ([FromBody] SchoolClass schoolClass, ITestingAppRepository repository) =>
        {
            await repository.UpdateSchoolClassAsync(schoolClass);
            await repository.SaveAsync();
            return Results.Created($"/classes/{schoolClass.Id}", schoolClass);
        })
            .WithTags("Classes");

        app.MapDelete("/classes/{id}", async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteSchoolClassAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("Classes");
    }
}