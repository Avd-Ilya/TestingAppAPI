using Microsoft.AspNetCore.Authorization;

public class UserAnswerApi {
    public void Register(WebApplication app) {
        app.MapGet("/user-answers", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetUserAnswersAsync()))
            .WithTags("UserAnswers");

        app.MapGet("/user-answers/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
            await repository.GetUserAnswerAsync(id) is UserAnswer userAnswer
            ? Results.Ok(userAnswer)
            : Results.NotFound())
            .WithTags("UserAnswers");

        app.MapPost("/user-answers", [Authorize] async ([FromBody] UserAnswer userAnswer, ITestingAppRepository repository) =>
        {
            await repository.InsertUserAnswerAsync(userAnswer);
            await repository.SaveAsync();
            return Results.Created($"/user-answers/{userAnswer.Id}", userAnswer);
        })
            .WithTags("UserAnswers");

        app.MapPost("/user-answer-with-options", [Authorize] async ([FromBody] UserAnswerRequest userAnswerRequest, ITestingAppRepository repository) =>
        {
            // var userAnswer = new UserAnswer();
            // userAnswer.PassedTestId = userAnswerRequest.PassedTestId;
            // userAnswer.QuestionId = userAnswerRequest.QuestionId;
            // var options = userAnswerRequest.AnswerOptionIds;
            // foreach (var option in options)
            // {
            //     var optionFromDb = await repository.GetAnswerOptionAsync(option);
            //     if(optionFromDb == null) continue;
            //     // userAnswer.AnswerOptions.Add(optionFromDb);
            // }
            // await repository.InsertUserAnswerAsync(userAnswer);
            // await repository.SaveAsync();
            // return Results.Created($"/user-answers/{userAnswer.Id}", userAnswer);
            return Results.NoContent();
        })
            .WithTags("UserAnswers");

        app.MapPut("/user-answers", [Authorize] async ([FromBody] UserAnswer userAnswer, ITestingAppRepository repository) =>
        {
            await repository.UpdateUserAnswerAsync(userAnswer);
            await repository.SaveAsync();
            return Results.Created($"/user-answers/{userAnswer.Id}", userAnswer);
        })
            .WithTags("UserAnswers");

        app.MapPut("/user-answers-with-options", [Authorize] async ([FromBody] UserAnswerRequest userAnswerRequest, ITestingAppRepository repository) =>
        {
            // var userAnswer = new UserAnswer();
            // if (userAnswerRequest.Id == null) return Results.NotFound();
            // userAnswer.Id = userAnswerRequest.Id ?? 0;
            // userAnswer.PassedTestId = userAnswerRequest.PassedTestId;
            // userAnswer.QuestionId = userAnswerRequest.QuestionId;
            // var options = userAnswerRequest.AnswerOptionIds;
            // foreach (var option in options)
            // {
            //     var optionFromDb = await repository.GetAnswerOptionAsync(option);
            //     if(optionFromDb == null) continue;
            //     // userAnswer.AnswerOptions.Add(optionFromDb);
            // }
            // await repository.UpdateUserAnswerAsync(userAnswer);
            // await repository.SaveAsync();
            // return Results.Created($"/user-answers/{userAnswer.Id}", userAnswer);
            return Results.NoContent();
        })
            .WithTags("UserAnswers");

        app.MapDelete("/user-answers/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeleteUserAnswerAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("UserAnswers");
    }
}