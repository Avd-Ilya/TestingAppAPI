public class TestingAppDb: DbContext
{
    public TestingAppDb(DbContextOptions<TestingAppDb> options) : base(options) {}
    public DbSet<User> Users => Set<User>();
    public DbSet<SchoolClass> SchoolClasses => Set<SchoolClass>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<Chapter> Chapters => Set<Chapter>();
    public DbSet<QuestionType> QuestionTypes => Set<QuestionType>();
    public DbSet<Question> Questions => Set<Question>();
    public DbSet<Test> Tests => Set<Test>();
}