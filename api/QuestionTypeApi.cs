using Microsoft.AspNetCore.Authorization;

public class QuestionTypeApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/question-types", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetQuestionTypesAsync()))
            .WithTags("QuestionTypes");

        app.MapGet("/question-types/{id}", async (int id, ITestingAppRepository repository) =>
            await repository.GetQuestionTypeAsync(id) is QuestionType questionType
            ? Results.Ok(questionType)
            : Results.NotFound())
            .WithTags("QuestionTypes");

        app.MapPost("/question-types", [Authorize] async ([FromBody] QuestionType questionType, ITestingAppRepository repository) =>
        {
            await repository.InsertQuestionTypeAsync(questionType);
            await repository.SaveAsync();
            return Results.Created($"/question-types/{questionType.Id}", questionType);
        })
            .WithTags("QuestionTypes");

        app.MapPut("/question-types", [Authorize] async ([FromBody] QuestionType questionType, ITestingAppRepository repository) =>
        {
            await repository.UpdateQuestionTypeAsync(questionType);
            await repository.SaveAsync();
            return Results.Created($"/question-types/{questionType.Id}", questionType);
        })
            .WithTags("QuestionTypes");

        app.MapDelete("/question-types/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteQuestionTypeAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("QuestionTypes");
    }
}