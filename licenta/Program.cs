using licenta.DbContexts;
using licenta.Services.InstitutionHierarchy;
using licenta.Services.Subjects;
using licenta.Services.Syllabuses;
using licenta.Services.Teachers;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EntityContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
builder.Services.AddScoped<IFieldOfStudyRepository, FieldOfStudyRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ISyllabusRepository, SyllabusRepository>();
builder.Services.AddScoped<ISyllabusTeacherRepository, SyllabusTeacherRepository>();
builder.Services.AddScoped<ISyllabusSubjectRepository, SyllabusSubjectRepository>();
builder.Services.AddScoped<ISubjectRepository, SubjectRepository>();
builder.Services.AddScoped<ISection1Repository, Section1Repository>();
builder.Services.AddScoped<ISection2Repository, Section2Repository>();
builder.Services.AddScoped<ISection3Repository, Section3Repository>();
builder.Services.AddScoped<ISection4Repository, Section4Repository>();
builder.Services.AddScoped<ISection5Repository, Section5Repository>();
builder.Services.AddScoped<ISection6Repository, Section6Repository>();
builder.Services.AddScoped<ISection7Repository, Section7Repository>();
builder.Services.AddScoped<ISection8ElementRepository, Section8ElementRepository>();
builder.Services.AddScoped<ISection8Repository, Section8Repository>();
builder.Services.AddScoped<ISection9Repository, Section9Repository>();
builder.Services.AddScoped<ISection10Repository, Section10Repository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
