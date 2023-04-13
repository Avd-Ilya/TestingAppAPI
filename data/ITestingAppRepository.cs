using System.Collections.Generic;
public interface ITestingAppRepository : IDisposable
{
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
    Task AddQuestionToTestAsync(int testId, int questionId);
    Task DeleteQuestionFromTestAsync(int testId, int questionId);

    //Test
    Task<List<Test>> GetTestsAsync();
    Task<Test> GetTestAsync(int id);
    Task InsertTestAsync(Test test);
    Task UpdateTestAsync(Test test);
    Task DeleteTestAsync(int id);


    Task SaveAsync();
}