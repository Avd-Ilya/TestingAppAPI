using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

public class PassedTestApi
{
    public void Register(WebApplication app)
    {
        app.MapGet("/passed-tests", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetPassedTestsAsync()))
            .WithTags("PassedTests");

        app.MapGet("/passed-tests-by-user", [Authorize] async (HttpContext http, ITestingAppRepository repository) =>
        {
            var token = await http.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            return Results.Ok(await repository.GetPassedTestsByUserAsync(userId));
        })
            .WithTags("PassedTests");

        app.MapGet("/passed-tests/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
            await repository.GetPassedTestAsync(id) is PassedTest passedTest
            ? Results.Ok(passedTest)
            : Results.NotFound())
            .WithTags("PassedTests");

        app.MapPost("/passed-tests", [Authorize] async ([FromBody] PassedTest passedTest, ITestingAppRepository repository) =>
        {
            await repository.InsertPassedTestAsync(passedTest);
            await repository.SaveAsync();
            return Results.Created($"/passed-tests/{passedTest.Id}", passedTest);
        })
            .WithTags("PassedTests");

        app.MapPost("/passed-tests-with-answers", [HttpGet][Authorize] async ([FromBody] PassedTestRequset passedTestRequset, HttpContext http, ITestingAppRepository repository) =>
        {
            var token = await http.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;

            var passedTest = new PassedTest();
            passedTest.Date = passedTestRequset.Date;
            passedTest.Result = passedTestRequset.Result;
            passedTest.TestId = passedTestRequset.TestId;
            passedTest.UserId = Guid.Parse(userId);
            passedTest.TrackedTestId = passedTestRequset.TrackedTestId;
            await repository.InsertPassedTestAsync(passedTest);
            await repository.SaveAsync();
            var answers = passedTestRequset.UserAnswerRequests;
            ICollection<UserAnswer> userAnswers = new HashSet<UserAnswer>();
            foreach (var item in answers)
            {
                var userAnswer = new UserAnswer();
                userAnswer.PassedTestId = passedTest.Id;
                userAnswer.QuestionId = item.QuestionId;
                await repository.InsertUserAnswerAsync(userAnswer);
                await repository.SaveAsync();

                var options = item.SelectedOptionRequests;
                foreach (var option in options)
                {
                    var selectedOption = new SelectedOption();
                    selectedOption.AnswerOtionId = option.AnswerOptionId;
                    selectedOption.Position = option.Position;
                    selectedOption.UserAnswerId = userAnswer.Id;
                    await repository.InsertSelectedOptionAsync(selectedOption);
                    await repository.SaveAsync();
                }
            }

            var test = await repository.GetTestAsync(passedTest.TestId ?? -1);
            var maxScore = test.Questions.Count();
            double result = 0;
            for (int i = 0; i < test.Questions.Count(); i++)
            {
                // if(passedTest.UserAnswers.ElementAt(i).SelectedOptions.Count == 0) { continue; }
                var question = test.Questions.ElementAt(i);
                var userAnswrs = passedTest.UserAnswers.ElementAt(i);
                switch (question.QuestionTypeId)
                {
                    case 1:
                        if (userAnswrs.SelectedOptions.FirstOrDefault()?.AnswerOtion?.IsCorrect ?? false)
                        {
                            result += 1.0;
                        }
                        break;
                    case 2:
                        double interimScore = 0;
                        var quantityCorrectAnswers = 0;
                        foreach (var answerOption in question.AnswerOptions)
                        {
                            if (answerOption.IsCorrect ?? false)
                            {
                                quantityCorrectAnswers += 1;
                            }
                        }
                        foreach (var selectedOption in userAnswrs.SelectedOptions)
                        {
                            if (selectedOption?.AnswerOtion?.IsCorrect ?? false)
                            {
                                interimScore += 1.0 / Convert.ToDouble(quantityCorrectAnswers);
                            }
                            else
                            {
                                interimScore = -1;
                            }
                        }
                        if (interimScore > 0)
                        {
                            result += interimScore;
                        }
                        break;
                    case 3:
                        var leftAnswerOptions = userAnswrs?.Question?.AnswerOptions.Where(x => x.LeftOptionId == null).ToList();
                        for (int z = 0; z < leftAnswerOptions?.Count(); z++)
                        {
                            if(userAnswrs?.SelectedOptions?.ElementAt(z)?.AnswerOtion?.LeftOptionId == leftAnswerOptions[z].Id) {
                                result += 1.0 / Convert.ToDouble(leftAnswerOptions.Count());
                            }
                        }
                        break;
                    case 4:
                        var quantityAnswerOptions = userAnswrs?.Question?.AnswerOptions.Count();
                        foreach (var selectedOption in userAnswrs.SelectedOptions)
                        {
                            if (selectedOption?.Position == selectedOption?.AnswerOtion?.Position)
                            {
                                result += 1.0 / Convert.ToDouble(quantityAnswerOptions);
                            }
                        }
                        break;
                    default:
                        break;
                }

            }

            passedTest.Result = (result / maxScore) * 100;
            await repository.UpdatePassedTestAsync(passedTest);
            await repository.SaveAsync();
            return Results.Created($"/passed-tests/{passedTest.Id}", passedTest);
        })
            .WithTags("PassedTests");

        app.MapPut("/passed-tests", [Authorize] async ([FromBody] PassedTest passedTest, ITestingAppRepository repository) =>
        {
            await repository.UpdatePassedTestAsync(passedTest);
            await repository.SaveAsync();
            return Results.Created($"/passed-tests/{passedTest.Id}", passedTest);
        })
            .WithTags("PassedTests");

        app.MapDelete("/passed-tests/{id}", [Authorize] async (int id, ITestingAppRepository repository) =>
        {
            await repository.DeletePassedTestAsync(id);
            await repository.SaveAsync();
            return Results.NoContent();
        })
            .WithTags("PassedTests");
    }
}