using TestingAppApi.Data;
using TestingAppApi.Auth;
using TestingAppApi.Users;

public class TestingAppRepository : ITestingAppRepository
{
    private readonly TestingAppDb _context;
    private readonly IJwtService _jwtService;
    public TestingAppRepository(TestingAppDb context, IJwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    //User

    public async Task<string> Login(User user)
    {
        var auth = await _context.Users.FirstOrDefaultAsync(x => x.Username == user.Username);

        if (auth is null)
            throw new Exception($"User with username {user.Username} doesn't exists");

        if (auth.Password != user.Password)
            throw new Exception($"Wrong password for user {user.Username}");

        return _jwtService.GenerateToken(auth);
    }

    public async Task<string> Register(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return _jwtService.GenerateToken(user);
    }

    public Task<List<User>> GetUsersAsync() =>
        _context.Users
        .ToListAsync();

    public async Task<User> GetUserAsync(String id) =>
        await _context.Users.FindAsync(new object[] { Guid.Parse(id) });

    public async Task<Boolean> UpdateUserAsync(User user)
    {
        var userFromDb = await _context.Users.FindAsync(new object[] { user.Id });
        if (userFromDb == null) return false;
        userFromDb.Fio = user.Fio;
        userFromDb.Username = user.Username;
        userFromDb.Password = user.Password;
        return true;
    }

    public async Task<User> GetUserByEmailAsync(String email)
    {
        var user = await _context.Users
        .FirstOrDefaultAsync(h => h.Username == email);
        return user;
    }
    public async Task<Boolean> DeleteUserAsync(String id)
    {
        var userFromDb = await _context.Users.FindAsync(new object[] { Guid.Parse(id) });
        if (userFromDb == null) return false;
        _context.Users.Remove(userFromDb);
        return true;
    }

    //SchooleClass
    public Task<List<SchoolClass>> GetSchoolClassesAsync() =>
        _context.SchoolClasses
        .ToListAsync();

    public async Task<SchoolClass> GetSchoolClassAsync(int id) =>
        await _context.SchoolClasses.FindAsync(new object[] { id });

    public async Task InsertSchoolClassAsync(SchoolClass schoolClass) =>
        await _context.SchoolClasses.AddAsync(schoolClass);

    public async Task UpdateSchoolClassAsync(SchoolClass schoolClass)
    {
        var schoolClassFromDb = await _context.SchoolClasses.FindAsync(new object[] { schoolClass.Id });
        if (schoolClassFromDb == null) return;
        schoolClassFromDb.Name = schoolClass.Name;
    }

    public async Task DeleteSchoolClassAsync(int id)
    {
        var schoolClassFromDb = await _context.SchoolClasses.FindAsync(new object[] { id });
        if (schoolClassFromDb == null) return;
        _context.SchoolClasses.Remove(schoolClassFromDb);
    }

    //Subject
    public Task<List<Subject>> GetSubjectsAsync() =>
        _context.Subjects
        .Include(x => x.SchoolClass)
        .ToListAsync();

    public Task<List<Subject>> GetSubjectsByClassAsync(int id) =>
        _context.Subjects
        .Where(x => x.SchoolClassId == id)
        .Include(x => x.SchoolClass)
        .ToListAsync();

    public async Task<Subject> GetSubjectAsync(int id)
    {
        var subject = await _context.Subjects
        .Include(x => x.SchoolClass)
        .FirstOrDefaultAsync(h => h.Id == id);
        return subject;
    }

    public async Task InsertSubjectAsync(Subject subject) =>
        await _context.Subjects.AddAsync(subject);

    public async Task UpdateSubjectAsync(Subject subject)
    {
        var subjectFromDb = await _context.Subjects.FindAsync(new object[] { subject.Id });
        if (subjectFromDb == null) return;
        subjectFromDb.Name = subject.Name;
        subjectFromDb.SchoolClass = subject.SchoolClass;
    }

    public async Task DeleteSubjectAsync(int id)
    {
        var subjectFromDb = await _context.Subjects.FindAsync(new object[] { id });
        if (subjectFromDb == null) return;
        _context.Subjects.Remove(subjectFromDb);
    }

    //Chapter
    public Task<List<Chapter>> GetChaptersAsync() =>
        _context.Chapters
        .Include(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .ToListAsync();

    public Task<List<Chapter>> GetChaptersBySubjectAsync(int id) =>
        _context.Chapters
        .Where(x => x.SubjectId == id)
        .Include(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .ToListAsync();

    public async Task<Chapter> GetChapterAsync(int id)
    {
        var chapter = await _context.Chapters
        .Include(x => x.Subject)
        .FirstOrDefaultAsync(h => h.Id == id);
        return chapter;
    }

    public async Task InsertChapterAsync(Chapter chapter) =>
        await _context.Chapters.AddAsync(chapter);

    public async Task UpdateChapterAsync(Chapter chapter)
    {
        var chapterFromDb = await _context.Chapters.FindAsync(new object[] { chapter.Id });
        if (chapterFromDb == null) return;
        chapterFromDb.Name = chapter.Name;
        chapterFromDb.Subject = chapter.Subject;
    }

    public async Task DeleteChapterAsync(int id)
    {
        var chapterFromDb = await _context.Chapters.FindAsync(new object[] { id });
        if (chapterFromDb == null) return;
        _context.Chapters.Remove(chapterFromDb);
    }

    //QuestionType
    public Task<List<QuestionType>> GetQuestionTypesAsync() =>
        _context.QuestionTypes
        .ToListAsync();

    public async Task<QuestionType> GetQuestionTypeAsync(int id) =>
        await _context.QuestionTypes.FindAsync(new object[] { id });

    public async Task InsertQuestionTypeAsync(QuestionType questionType) =>
        await _context.QuestionTypes.AddAsync(questionType);

    public async Task UpdateQuestionTypeAsync(QuestionType questionType)
    {
        var questionTypeFromDb = await _context.QuestionTypes.FindAsync(new object[] { questionType.Id });
        if (questionTypeFromDb == null) return;
        questionTypeFromDb.Name = questionType.Name;
    }

    public async Task DeleteQuestionTypeAsync(int id)
    {
        var questionTypeFromDb = await _context.QuestionTypes.FindAsync(new object[] { id });
        if (questionTypeFromDb == null) return;
        _context.QuestionTypes.Remove(questionTypeFromDb);
    }

    //Question
    public Task<List<Question>> GetQuestionsAsync() =>
        _context.Questions
        .Include(x => x.QuestionType)
        .Include(x => x.AnswerOptions)
        .ToListAsync();

    public async Task<Question> GetQuestionAsync(int id)
    {
        var question = await _context.Questions
        .Include(x => x.QuestionType)
        .Include(x => x.AnswerOptions)
        .FirstOrDefaultAsync(h => h.Id == id);
        return question;
    }

    public async Task InsertQuestionAsync(Question question) =>
        await _context.Questions.AddAsync(question);

    public async Task UpdateQuestionAsync(Question question)
    {
        var questionFromDb = await _context.Questions.FindAsync(new object[] { question.Id });
        if (questionFromDb == null) return;
        questionFromDb.Task = question.Task;
        questionFromDb.QuestionTypeId = question.QuestionTypeId;
        questionFromDb.TestId = question.TestId;
    }

    public async Task DeleteQuestionAsync(int id)
    {
        var questionFromDb = await _context.Questions.FindAsync(new object[] { id });
        if (questionFromDb == null) return;
        _context.Questions.Remove(questionFromDb);
    }

    // public async Task AddQuestionToTestAsync(int testId, int questionId)
    // {   
    //     var test = await _context.Tests.FindAsync(new object[] {testId});
    // var question = await _context.Questions.FindAsync(new object[] {questionId});
    // if (test == null || question == null) return;
    //     test.Questions.Add(question);
    // }

    // public async Task DeleteQuestionFromTestAsync(int testId, int questionId)
    // {
    //     var question = await _context.Questions.FindAsync(new object[] {questionId});
    //     var test = await _context.Tests
    //     .Include(x => x.Questions)
    //     .FirstOrDefaultAsync(h => h.Id == testId);

    //     if (test == null || question == null) return;
    //     test.Questions.Remove(question);
    // }

    //Test
    public Task<List<Test>> GetTestsAsync() =>
        _context.Tests
        .Include(x => x.Chapter)
        .ThenInclude(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .Include(x => x.Questions)
        .ThenInclude(x => x.QuestionType)
        .Include(x => x.Questions)
        .ThenInclude(x => x.AnswerOptions)
        .ToListAsync();

    public Task<List<Test>> GetTestsByChapterAsync(int id) =>
        _context.Tests
        .Where(x => x.ChapterId == id)
        .Include(x => x.Chapter)
        .ThenInclude(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .Include(x => x.Questions)
        .ThenInclude(x => x.QuestionType)
        .Include(x => x.Questions)
        .ThenInclude(x => x.AnswerOptions)
        .ToListAsync();

    public async Task<Test> GetTestAsync(int id)
    {
        var test = await _context.Tests
        .Include(x => x.Chapter)
        .ThenInclude(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .Include(x => x.Questions)
        .ThenInclude(x => x.QuestionType)
        .Include(x => x.Questions)
        .ThenInclude(x => x.AnswerOptions)
        .FirstOrDefaultAsync(h => h.Id == id);
        return test;
    }

    public async Task InsertTestAsync(Test test) =>
        await _context.Tests.AddAsync(test);

    public async Task UpdateTestAsync(Test test)
    {
        var testFromDb = await _context.Tests.FindAsync(new object[] { test.Id });
        if (testFromDb == null) return;
        testFromDb.Topic = test.Topic;
        testFromDb.ChapterId = test.ChapterId;
    }

    public async Task DeleteTestAsync(int id)
    {
        var testFromDb = await _context.Tests.FindAsync(new object[] { id });
        if (testFromDb == null) return;
        _context.Tests.Remove(testFromDb);
    }

    //Answer option
    public Task<List<AnswerOption>> GetAnswerOptionsAsync() =>
        _context.AnswerOptions
        .ToListAsync();

    public async Task<AnswerOption> GetAnswerOptionAsync(int id)
    {
        var answerOption = await _context.AnswerOptions
        .FirstOrDefaultAsync(h => h.Id == id);
        return answerOption;
    }

    public async Task InsertAnswerOptionAsync(AnswerOption answerOption) =>
       await _context.AnswerOptions.AddAsync(answerOption);

    public async Task UpdateAnswerOptionAsync(AnswerOption answerOption)
    {
        var answerOptionFromDb = await _context.AnswerOptions.FindAsync(new object[] { answerOption.Id });
        if (answerOptionFromDb == null) return;
        answerOptionFromDb.Text = answerOption.Text;
        answerOptionFromDb.IsCorrect = answerOption.IsCorrect;
        answerOptionFromDb.QuestionId = answerOption.QuestionId;
        answerOptionFromDb.Position = answerOption.Position;
        answerOptionFromDb.LeftOptionId = answerOption.LeftOptionId;
    }

    public async Task DeleteAnswerOptionAsync(int id)
    {
        var answerOptionFromDb = await _context.AnswerOptions.FindAsync(new object[] { id });
        if (answerOptionFromDb == null) return;
        _context.AnswerOptions.Remove(answerOptionFromDb);
    }

    //Passed test
    public Task<List<PassedTest>> GetPassedTestsAsync() =>
        _context.PassedTests
        .Include(x => x.UserAnswers)
        .ThenInclude(x => x.Question)
        .ThenInclude(x => x.QuestionType)
        .Include(x => x.UserAnswers)
        .ThenInclude(x => x.Question)
        .ThenInclude(x => x.AnswerOptions)
        .Include(x => x.UserAnswers)
        .ThenInclude(x => x.SelectedOptions)
        .ToListAsync();

    public async Task<List<PassedTest>> GetPassedTestsByUserAsync(String id)
    {
        return await _context.PassedTests
        .Where(x => x.UserId == Guid.Parse(id))
        .Include(x => x.Test)
        .ThenInclude(x => x.Chapter)
        .ThenInclude(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .Include(x => x.UserAnswers)
        .ThenInclude(x => x.Question)
        .ThenInclude(x => x.QuestionType)
        .Include(x => x.UserAnswers)
        .ThenInclude(x => x.Question)
        .ThenInclude(x => x.AnswerOptions)
        .Include(x => x.UserAnswers)
        .ThenInclude(x => x.SelectedOptions)
        .ToListAsync();
    }

    public async Task<List<PassedTest>> GetPassedTestsByTrackedTestIdAsync(int id)
    {
        return await _context.PassedTests
        .Where(x => x.TrackedTestId == id)
        .Include(x => x.Test)
        .ThenInclude(x => x.Chapter)
        .ThenInclude(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .Include(x => x.UserAnswers)
        .ThenInclude(x => x.Question)
        .ThenInclude(x => x.QuestionType)
        .Include(x => x.UserAnswers)
        .ThenInclude(x => x.Question)
        .ThenInclude(x => x.AnswerOptions)
        .Include(x => x.UserAnswers)
        .ThenInclude(x => x.SelectedOptions)
        .ToListAsync();
    }

    public async Task<PassedTest> GetPassedTestAsync(int id)
    {
        var passedTest = await _context.PassedTests
        .FirstOrDefaultAsync(h => h.Id == id);
        return passedTest;
    }

    public async Task InsertPassedTestAsync(PassedTest passedTest) =>
       await _context.PassedTests.AddAsync(passedTest);

    public async Task UpdatePassedTestAsync(PassedTest passedTest)
    {
        var passedTestFromDb = await _context.PassedTests.FindAsync(new object[] { passedTest.Id });
        if (passedTestFromDb == null) return;
        passedTestFromDb.Date = passedTest.Date;
        passedTestFromDb.Result = passedTest.Result;
        passedTestFromDb.TestId = passedTest.TestId;
        passedTestFromDb.UserId = passedTest.UserId;
        passedTestFromDb.UserAnswers = passedTest.UserAnswers;
    }

    public async Task DeletePassedTestAsync(int id)
    {
        var passedTestFromDb = await _context.PassedTests.FindAsync(new object[] { id });
        if (passedTestFromDb == null) return;
        _context.PassedTests.Remove(passedTestFromDb);
    }

    //User answer
    public Task<List<UserAnswer>> GetUserAnswersAsync() =>
        _context.UserAnswers
        .Include(x => x.Question)
        .ThenInclude(x => x.QuestionType)
        .Include(x => x.Question)
        .ThenInclude(x => x.AnswerOptions)
        .Include(x => x.SelectedOptions)
        .ThenInclude(x => x.AnswerOtion)
        .ToListAsync();

    public Task<List<UserAnswer>> GetUserAnswersByPassedTestAsync(int id) =>
        _context.UserAnswers
        .Where(x => x.PassedTestId == id)
        .Include(x => x.Question)
        .ThenInclude(x => x.QuestionType)
        .Include(x => x.Question)
        .ThenInclude(x => x.AnswerOptions)
        .Include(x => x.SelectedOptions)
        .ThenInclude(x => x.AnswerOtion)
        .ToListAsync();
    public async Task<UserAnswer> GetUserAnswerAsync(int id)
    {
        var userAnswer = await _context.UserAnswers
        .FirstOrDefaultAsync(h => h.Id == id);
        return userAnswer;
    }

    public async Task InsertUserAnswerAsync(UserAnswer userAnswer)
    {
        await _context.UserAnswers.AddAsync(userAnswer);
        // var options = userAnswer.AnswerOptions;
        // userAnswer.AnswerOptions.Clear();
        // Console.Write("test");
        // Console.Write(options.Count());
        // await _context.UserAnswers.AddAsync(userAnswer);
        // foreach (var item in options)
        // {
        //     Console.Write(item.Id);
        //     var option = await _context.AnswerOptions.FindAsync(new object[] {item.Id});
        //     var userAnswerFromdb = await _context.UserAnswers.FindAsync(new object[] {userAnswer.Id});
        //     if (option == null || userAnswerFromdb == null) break;
        //     Console.Write("true");
        //     userAnswerFromdb.AnswerOptions.Add(option);
        // }        
    }
    public async Task UpdateUserAnswerAsync(UserAnswer userAnswer)
    {
        var userAnswerFromDb = await _context.UserAnswers.FindAsync(new object[] { userAnswer.Id });
        if (userAnswerFromDb == null) return;
        userAnswerFromDb.QuestionId = userAnswer.QuestionId;
        userAnswerFromDb.PassedTestId = userAnswer.PassedTestId;
        // userAnswerFromDb.AnswerOptions.Clear();
        // userAnswerFromDb.AnswerOptions = userAnswer.AnswerOptions;
    }

    public async Task DeleteUserAnswerAsync(int id)
    {
        var userAnswerFromDb = await _context.UserAnswers.FindAsync(new object[] { id });
        if (userAnswerFromDb == null) return;
        _context.UserAnswers.Remove(userAnswerFromDb);
    }

    //Selected option
    public Task<List<SelectedOption>> GetSelectedOptionsAsync() =>
        _context.SelectedOptions
        .ToListAsync();

    public Task<List<SelectedOption>> GetSelectedOptionsByUserAnswerAsync(int id) =>
        _context.SelectedOptions
        .Where(x => x.UserAnswerId == id)
        .ToListAsync();

    public async Task<SelectedOption> GetSelectedOptionAsync(int id)
    {
        var selectedOption = await _context.SelectedOptions
        .FirstOrDefaultAsync(h => h.Id == id);
        return selectedOption;
    }

    public async Task InsertSelectedOptionAsync(SelectedOption selectedOption)
    {
        await _context.SelectedOptions.AddAsync(selectedOption);
    }

    public async Task UpdateSelectedOptionAsync(SelectedOption selectedOption)
    {
        var selectedOptionFromDb = await _context.SelectedOptions.FindAsync(new object[] { selectedOption.Id });
        if (selectedOptionFromDb == null) return;
        selectedOptionFromDb.Position = selectedOption.Position;
        selectedOptionFromDb.AnswerOtionId = selectedOption.AnswerOtionId;
        selectedOptionFromDb.UserAnswerId = selectedOption.UserAnswerId;
    }

    public async Task DeleteSelectedOptionAsync(int id)
    {
        var selectedOptionFromDb = await _context.SelectedOptions.FindAsync(new object[] { id });
        if (selectedOptionFromDb == null) return;
        _context.SelectedOptions.Remove(selectedOptionFromDb);
    }

    //Tracked test
    public Task<List<TrackedTest>> GetTrackedTestsAsync() =>
        _context.TrackedTests
        .Include(x => x.Test)
        .Include(x => x.User)
        .ToListAsync();
    public async Task<List<TrackedTest>> GetTrackedTestsForUserAsync(string id)
    {
        return await _context.TrackedTests
        .Where(x => x.UserId == Guid.Parse(id))
        .Include(x => x.Test)
        .ThenInclude(x => x.Chapter)
        .ThenInclude(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .ToListAsync();
    }
    public async Task<TrackedTest> GetTrackedTestAsync(int id)
    {
        var trackedTest = await _context.TrackedTests
        .Include(x => x.User)
        .FirstOrDefaultAsync(h => h.Id == id);
        return trackedTest;
    }
    public async Task<TrackedTest?> GetTrackedTestByKeyAsync(string key)
    {
        var trackedTest = await _context.TrackedTests
        .Include(x => x.User)
        .FirstOrDefaultAsync(h => h.Key == key);
        return trackedTest;
    }

    public async Task InsertTrackedTestAsync(TrackedTest trackedTest) =>
        await _context.TrackedTests.AddAsync(trackedTest);

    public async Task UpdateTrackedTestAsync(TrackedTest trackedTest)
    {
        var trackedTestFromDb = await _context.TrackedTests.FindAsync(new object[] { trackedTest.Id });
        if (trackedTestFromDb == null) return;
        trackedTestFromDb.Key = trackedTest.Key;
        trackedTestFromDb.DateCreation = trackedTest.DateCreation;
        trackedTestFromDb.Description = trackedTest.Description;
        trackedTestFromDb.TestId = trackedTest.TestId;
        trackedTestFromDb.UserId = trackedTest.UserId;
    }

    public async Task DeleteTrackedTestAsync(int id)
    {
        var trackedTestFromDb = await _context.TrackedTests.FindAsync(new object[] { id });
        if (trackedTestFromDb == null) return;
        _context.TrackedTests.Remove(trackedTestFromDb);
    }

    //Save
    public async Task SaveAsync() =>
        await _context.SaveChangesAsync();

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}