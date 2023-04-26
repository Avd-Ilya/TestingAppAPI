public class QuestionApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/questions", async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetQuestionsAsync()))
            .WithTags("Questions");

        app.MapGet("/questions/{id}", async (int id, ITestingAppRepository repository) =>
            await repository.GetQuestionAsync(id) is Question question
            ? Results.Ok(question)
            : Results.NotFound())
            .WithTags("Questions");

        app.MapPost("/questions", async ([FromBody] Question question, ITestingAppRepository repository) =>
        {
            await repository.InsertQuestionAsync(question);
            await repository.SaveAsync();
            return Results.Created($"/questions/{question.Id}", question);
        })
            .WithTags("Questions");

        app.MapPut("/questions", async ([FromBody] Question question, ITestingAppRepository repository) =>
        {
            await repository.UpdateQuestionAsync(question);
            await repository.SaveAsync();
            return Results.Created($"/questions/{question.Id}", question);
        })
            .WithTags("Questions");

        app.MapDelete("/questions/{id}", async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteQuestionAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("Questions");

        // app.MapPost("/add-question-to-test/{testId}/{questionId}", async (int testId, int questionId, ITestingAppRepository repository) =>
        // {
        //     await repository.AddQuestionToTestAsync(testId, questionId);
        //     await repository.SaveAsync();
        //     return Results.Created($"/tests/{testId}", await repository.GetTestAsync(testId));
        // })
        //     .WithTags("Questions");
        
        // app.MapPost("/delete-question-from-test/{testId}/{questionId}", async (int testId, int questionId, ITestingAppRepository repository) =>
        // {
        //     await repository.DeleteQuestionFromTestAsync(testId, questionId);
        //     await repository.SaveAsync();
        //     return Results.Created($"/tests/{testId}", await repository.GetTestAsync(testId));
        // })
        //     .WithTags("Questions");
    }
}