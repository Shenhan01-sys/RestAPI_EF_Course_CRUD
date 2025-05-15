using SIMPLEAPI_Instructor.data;
using SIMPLEAPI_Instructor.models;
using SIMPLEAPI_Instructor.interfaces;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        //DI
        builder.Services.AddScoped<ICourse, CourseEF>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGet("api/v1/Courses", (ICourse courseData) =>
        {
            var courses = courseData.GetCourses();
            return courses;
        });

        app.MapGet("api/v1/Courses/{id}", (ICourse courseData, int id) =>
        {
            var course = courseData.GetCourseByID(id);
            return course is not null ? Results.Ok(course) : Results.NotFound();
        });

        app.MapPost("api/v1/Courses", (ICourse courseData, Course course) =>
        {
            courseData.AddCourse(course);
            return Results.Created($"/api/v1/Courses/{course.CourseId}", course);
        });

        app.MapDelete("api/v1/Courses/{id}", (ICourse courseData, int id) =>
        {
            var course = courseData.GetCourseByID(id);
            if (course is null)
            {
                return Results.NotFound();
            }

            courseData.DeleteCourse(id);
            return Results.NoContent();
        });

        app.Run();
    }
}
