using System.ComponentModel.Design;
using TestingAppApi.Users;

namespace TestingAppApi.Data;

public class TestingAppDb: DbContext
{
    public TestingAppDb(DbContextOptions<TestingAppDb> options) : base(options) {
        Database.EnsureCreated();
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<SchoolClass> SchoolClasses => Set<SchoolClass>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Chapter> Chapters => Set<Chapter>();
    public DbSet<QuestionType> QuestionTypes => Set<QuestionType>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Test> Tests => Set<Test>();
    public DbSet<AnswerOption> AnswerOptions => Set<AnswerOption>();
    public DbSet<PassedTest> PassedTests => Set<PassedTest>();
    public DbSet<UserAnswer> UserAnswers => Set<UserAnswer>();
    public DbSet<SelectedOption> SelectedOptions => Set<SelectedOption>();
}