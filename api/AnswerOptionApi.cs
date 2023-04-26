public class AnswerOptionApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/answer-options", async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetAnswerOptionsAsync()))
            .WithTags("AnswerOptions");

        app.MapGet("/answer-options/{id}", async (int id, ITestingAppRepository repository) =>
            await repository.GetAnswerOptionAsync(id) is AnswerOption answerOption
            ? Results.Ok(answerOption)
            : Results.NotFound())
            .WithTags("AnswerOptions");

        app.MapPost("/answer-options", async ([FromBody] AnswerOption answerOption, ITestingAppRepository repository) =>
        {
            await repository.InsertAnswerOptionAsync(answerOption);
            await repository.SaveAsync();
            return Results.Created($"/answer-options/{answerOption.Id}", answerOption);
        })
            .WithTags("AnswerOptions");

        app.MapPut("/answer-options", async ([FromBody] AnswerOption answerOption, ITestingAppRepository repository) =>
        {
            await repository.UpdateAnswerOptionAsync(answerOption);
            await repository.SaveAsync();
            return Results.Created($"/answer-options/{answerOption.Id}", answerOption);
        })
            .WithTags("AnswerOptions");

        app.MapDelete("/answer-options/{id}", async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteAnswerOptionAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("AnswerOptions");
    }
}