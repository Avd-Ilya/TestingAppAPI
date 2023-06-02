using FluentValidation;
using Microsoft.OpenApi.Models;
using TestingAppApi;
using TestingAppApi.Extensions;
using TestingAppApi.Users;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddDatabase();
builder.Services.AddServices();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(IAssemblyMarker));
builder.Services.AddSwagger();
builder.Services.AddJwt(configuration);

var app = builder.Build();

new UserApi().Register(app);
new SchoolClassApi().Register(app);
new SubjectApi().Register(app);
new ChapterApi().Register(app);
new TestApi().Register(app);
new QuestionApi().Register(app);
new QuestionTypeApi().Register(app);
new AnswerOptionApi().Register(app);
new UserAnswerApi().Register(app);
new PassedTestApi().Register(app);
new SelectedOptionApi().Register(app);
new TrackedTestApi().Register(app);

app.UseSwaggerApp();
app.DatabaseEnsureCreated();
app.UseHttpsRedirection();
app.UseAuth();

app.Run();