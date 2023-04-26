using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

public class PassedTestApi {
        public void Register(WebApplication app)
    {
        app.MapGet("/passed-tests", [Authorize] async (ITestingAppRepository repository) =>
            Results.Ok(await repository.GetPassedTestsAsync()))
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
        
        app.MapPost("/passed-tests-with-answers", [HttpGet] [Authorize] async ([FromBody] PassedTestRequset passedTestRequset, HttpContext http, ITestingAppRepository repository) =>
        {
            var token = await http.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token");
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var userId = decodedToken.Claims.FirstOrDefault(c => c.Type == "id")?.Value;
            Console.WriteLine(userId);
            Console.WriteLine(Guid.Parse(userId));

            var passedTest = new PassedTest();
            passedTest.Date = passedTestRequset.Date;
            passedTest.Result = passedTestRequset.Result;
            passedTest.TestId = passedTestRequset.TestId;
            passedTest.UserId = Guid.Parse(userId);
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
            return Results.Created($"/passed-tests/{passedTest.Id}", passedTest);
            // return Results.NoContent();
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