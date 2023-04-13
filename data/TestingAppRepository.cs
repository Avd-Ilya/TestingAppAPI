public class TestingAppRepository : ITestingAppRepository
{
    private readonly TestingAppDb _context;
    public TestingAppRepository(TestingAppDb context)
    {
        _context = context;
    }

    //SchooleClass
    public Task<List<SchoolClass>> GetSchoolClassesAsync() =>
        _context.SchoolClasses
        .ToListAsync();

    public async Task<SchoolClass> GetSchoolClassAsync(int id) =>
        await _context.SchoolClasses.FindAsync(new object[]{id});

    public async Task InsertSchoolClassAsync(SchoolClass schoolClass) =>
        await _context.SchoolClasses.AddAsync(schoolClass);

    public async Task UpdateSchoolClassAsync(SchoolClass schoolClass)
    {
        var schoolClassFromDb = await _context.SchoolClasses.FindAsync(new object[]{schoolClass.Id});
        if (schoolClassFromDb == null) return;
        schoolClassFromDb.Name = schoolClass.Name;
    }
        
    public async Task DeleteSchoolClassAsync(int id)
    {
        var schoolClassFromDb = await _context.SchoolClasses.FindAsync(new object[]{id});
        if (schoolClassFromDb == null) return;
        _context.SchoolClasses.Remove(schoolClassFromDb);
    }

    //Subject
    public Task<List<Subject>> GetSubjectsAsync() =>
        _context.Subjects
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
        var subjectFromDb = await _context.Subjects.FindAsync(new object[]{subject.Id});
        if (subjectFromDb == null) return;
        subjectFromDb.Name = subject.Name;
        subjectFromDb.SchoolClass = subject.SchoolClass;
    }
        
    public async Task DeleteSubjectAsync(int id)
    {
        var subjectFromDb = await _context.Subjects.FindAsync(new object[]{id});
        if (subjectFromDb == null) return;
        _context.Subjects.Remove(subjectFromDb);
    }

    //Chapter
    public Task<List<Chapter>> GetChaptersAsync() =>
        _context.Chapters
        .Include(x => x.Subject)
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
        var chapterFromDb = await _context.Chapters.FindAsync(new object[]{chapter.Id});
        if (chapterFromDb == null) return;
        chapterFromDb.Name = chapter.Name;
        chapterFromDb.Subject = chapter.Subject;
    }
        
    public async Task DeleteChapterAsync(int id)
    {
        var chapterFromDb = await _context.Chapters.FindAsync(new object[]{id});
        if (chapterFromDb == null) return;
        _context.Chapters.Remove(chapterFromDb);
    }

    //QuestionType
    public Task<List<QuestionType>> GetQuestionTypesAsync() =>
        _context.QuestionTypes
        .ToListAsync();

    public async Task<QuestionType> GetQuestionTypeAsync(int id) =>
        await _context.QuestionTypes.FindAsync(new object[]{id});

    public async Task InsertQuestionTypeAsync(QuestionType questionType) =>
        await _context.QuestionTypes.AddAsync(questionType);

    public async Task UpdateQuestionTypeAsync(QuestionType questionType)
    {
        var questionTypeFromDb = await _context.QuestionTypes.FindAsync(new object[]{questionType.Id});
        if (questionTypeFromDb == null) return;
        questionTypeFromDb.Name = questionType.Name;
    }

    public async Task DeleteQuestionTypeAsync(int id)
    {
        var questionTypeFromDb = await _context.QuestionTypes.FindAsync(new object[]{id});
        if (questionTypeFromDb == null) return;
        _context.QuestionTypes.Remove(questionTypeFromDb);
    }

    //Question
    public Task<List<Question>> GetQuestionsAsync() =>
        _context.Questions
        .Include(x => x.QuestionType)
        .ToListAsync();

    public async Task<Question> GetQuestionAsync(int id) 
    {
        var question = await _context.Questions
        .Include(x => x.QuestionType)
        .FirstOrDefaultAsync(h => h.Id == id);
        return question;
    }

    public async Task InsertQuestionAsync(Question question) =>
        await _context.Questions.AddAsync(question);

    public async Task UpdateQuestionAsync(Question question)
    {
        var questionFromDb = await _context.Questions.FindAsync(new object[]{question.Id});
        if (questionFromDb == null) return;
        questionFromDb.Task = question.Task;
        questionFromDb.QuestionTypeId = question.QuestionTypeId;
    }

    public async Task DeleteQuestionAsync(int id)
    {
        var questionFromDb = await _context.Questions.FindAsync(new object[]{id});
        if (questionFromDb == null) return;
        _context.Questions.Remove(questionFromDb);
    }

    public async Task AddQuestionToTestAsync(int testId, int questionId)
    {   
        var test = await _context.Tests.FindAsync(new object[] {testId});
        var question = await _context.Questions.FindAsync(new object[] {questionId});
        if (test == null || question == null) return;
        test.Questions.Add(question);
    }

    public async Task DeleteQuestionFromTestAsync(int testId, int questionId)
    {
        var question = await _context.Questions.FindAsync(new object[] {questionId});
        var test = await _context.Tests
        .Include(x => x.Questions)
        .FirstOrDefaultAsync(h => h.Id == testId);

        if (test == null || question == null) return;
        test.Questions.Remove(question);
    }
    
    //Test
    public Task<List<Test>> GetTestsAsync() =>
        _context.Tests
        .Include(x => x.Chapter)
        .ThenInclude(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .Include(x => x.Questions)
        .ThenInclude(x => x.QuestionType)
        .ToListAsync();

    public async Task<Test> GetTestAsync(int id) 
    {
        var test = await _context.Tests
        .Include(x => x.Chapter)
        .ThenInclude(x => x.Subject)
        .ThenInclude(x => x.SchoolClass)
        .Include(x => x.Questions)
        .FirstOrDefaultAsync(h => h.Id == id);
        return test;
    }

    public async Task InsertTestAsync(Test test) =>
        await _context.Tests.AddAsync(test);

    public async Task UpdateTestAsync(Test test)
    {
        var testFromDb = await _context.Questions.FindAsync(new object[]{test.Id});
        if (testFromDb == null) return;
        testFromDb.Task = test.Topic;
        testFromDb.QuestionTypeId = test.ChapterId;
    }

    public async Task DeleteTestAsync(int id)
    {
        var testFromDb = await _context.Tests.FindAsync(new object[]{id});
        if (testFromDb == null) return;
        _context.Tests.Remove(testFromDb);
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