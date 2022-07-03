using licenta.DbContexts;
using licenta.Services.Audits;
using licenta.Services.InstitutionHierarchy;
using licenta.Services.Subjects;
using licenta.Services.Syllabuses;
using licenta.Services.Teachers;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                });
});

builder.Services.AddDbContext<EntityContext>();
builder.Services.AddScoped<IInstitutionRepository, InstitutionRepository>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IFacultyRepository, FacultyRepository>();
builder.Services.AddScoped<IFieldOfStudyRepository, FieldOfStudyRepository>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ISyllabusRepository, SyllabusRepository>();
builder.Services.AddScoped<ISyllabusTeacherRepository, SyllabusTeacherRepository>();
builder.Services.AddScoped<ISyllabusSubjectRepository, SyllabusSubjectRepository>();
builder.Services.AddScoped<ISyllabusVersionRepository, SyllabusVersionRepository>();
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
builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Authentication:Issuer"],
        ValidAudience = builder.Configuration["Authentication:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"])
            )
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustBeAdmin", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin");
    });
    options.AddPolicy("MustBeAtLeastEditor", policy =>
     {
         policy.RequireAuthenticatedUser();
         policy.RequireClaim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Editor", "Admin");
     });
}
);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
