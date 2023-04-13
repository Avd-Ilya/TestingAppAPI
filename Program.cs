var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);

var app = builder.Build();

Configure(app);

new SchoolClassApi().Register(app);
new SubjectApi().Register(app);
new ChapterApi().Register(app);
new TestApi().Register(app);
new QuestionApi().Register(app);
new QuestionTypeApi().Register(app);


app.Run();

void RegisterServices(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();

    // SqlConnectionStringBuilder builderr = new SqlConnectionStringBuilder();
    // builderr.DataSource = "localhost";
    // builderr.UserID = "sa";             
    // builderr.Password = "reallyStrongPwd123";     
    // builderr.InitialCatalog = "ProductDB";

    services.AddDbContext<TestingAppDb>(options => 
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
        //local DB
        //options.UseSqlServer(builderr.ConnectionString);

        //host Db
        // options.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
    });

    services.AddScoped<ITestingAppRepository, TestingAppRepository>();

    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });
}

void Configure(WebApplication app)
{
    app.UseCors("AllowAll");
    app.UseSwagger();
    app.UseSwaggerUI(); 

    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<TestingAppDb>();
        // db.Database.EnsureDeleted();
        db.Database.EnsureCreated();
    }

    app.UseHttpsRedirection();
}