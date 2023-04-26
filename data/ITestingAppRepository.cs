using System.Collections.Generic;
using TestingAppApi.Users;

public interface ITestingAppRepository : IDisposable
{
    //User
    public Task<string> Login(User user);
    public Task<string> Register(User user);
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserAsync(String id);
    Task<Boolean> UpdateUserAsync(User user);
    Task<Boolean> DeleteUserAsync(String id);


    //Class
    Task<List<SchoolClass>> GetSchoolClassesAsync();
    Task<SchoolClass> GetSchoolClassAsync(int id);
    Task InsertSchoolClassAsync(SchoolClass schoolClass);
    Task UpdateSchoolClassAsync(SchoolClass schoolClass);
    Task DeleteSchoolClassAsync(int id);

    //Subject
    Task<List<Subject>> GetSubjectsAsync();
    Task<Subject> GetSubjectAsync(int id);
    Task InsertSubjectAsync(Subject subject);
    Task UpdateSubjectAsync(Subject subject);
    Task DeleteSubjectAsync(int id);

    //Chapter
    Task<List<Chapter>> GetChaptersAsync();
    Task<Chapter> GetChapterAsync(int id);
    Task InsertChapterAsync(Chapter chapter);
    Task UpdateChapterAsync(Chapter chapter);
    Task DeleteChapterAsync(int id);

    //QuestionType
    Task<List<QuestionType>> GetQuestionTypesAsync();
    Task<QuestionType> GetQuestionTypeAsync(int id);
    Task InsertQuestionTypeAsync(QuestionType questionType);
    Task UpdateQuestionTypeAsync(QuestionType questionType);
    Task DeleteQuestionTypeAsync(int id);

    //Question
    Task<List<Question>> GetQuestionsAsync();
    Task<Question> GetQuestionAsync(int id);
    Task InsertQuestionAsync(Question question);
    Task UpdateQuestionAsync(Question question);
    Task DeleteQuestionAsync(int id);
    // Task AddQuestionToTestAsync(int testId, int questionId);
    // Task DeleteQuestionFromTestAsync(int testId, int questionId);

    //Test
    Task<List<Test>> GetTestsAsync();
    Task<Test> GetTestAsync(int id);
    Task InsertTestAsync(Test test);
    Task UpdateTestAsync(Test test);
    Task DeleteTestAsync(int id);

    //Answer option
    Task<List<AnswerOption>> GetAnswerOptionsAsync();
    Task<AnswerOption> GetAnswerOptionAsync(int id);
    Task InsertAnswerOptionAsync(AnswerOption answerOption);
    Task UpdateAnswerOptionAsync(AnswerOption answerOption);
    Task DeleteAnswerOptionAsync(int id);

    //Passed test
    Task<List<PassedTest>> GetPassedTestsAsync();
    Task<PassedTest> GetPassedTestAsync(int id);
    Task InsertPassedTestAsync(PassedTest passedTest);
    Task UpdatePassedTestAsync(PassedTest passedTest);
    Task DeletePassedTestAsync(int id);

    //User answer
    Task<List<UserAnswer>> GetUserAnswersAsync();
    Task<UserAnswer> GetUserAnswerAsync(int id);
    Task InsertUserAnswerAsync(UserAnswer userAnswer);
    Task UpdateUserAnswerAsync(UserAnswer userAnswer);
    Task DeleteUserAnswerAsync(int id);

    //Selected option
    Task<List<SelectedOption>> GetSelectedOptionsAsync();
    Task<SelectedOption> GetSelectedOptionAsync(int id);
    Task InsertSelectedOptionAsync(SelectedOption selectedOption);
    Task UpdateSelectedOptionAsync(SelectedOption selectedOption);
    Task DeleteSelectedOptionAsync(int id);

    Task SaveAsync();
}