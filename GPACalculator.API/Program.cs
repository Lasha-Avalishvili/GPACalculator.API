using GPACalculator.API.Db;
using GPACalculator.API.Repositories;
using GPACalculator.API.Validations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(c =>
    c.UseSqlServer(builder.Configuration.GetConnectionString("GetString")));


builder.Services.AddControllers();

builder.Services.AddScoped<AddGradeValidator>();   // 
builder.Services.AddScoped<AddStudentValidator>();
builder.Services.AddScoped<AddSubjectValidator>();

builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();
builder.Services.AddTransient<IGradeRepository, GradeRepository>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ავტომატურად შექმნის ბაზას თუ არ არსებობს.
using var scope = app.Services.CreateScope(); 
var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
dbContext!.Database.EnsureCreated();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
