using SIMPLEAPI_Instructor.data;
using SIMPLEAPI_Instructor.models;
using SIMPLEAPI_Instructor.interfaces;
using Microsoft.EntityFrameworkCore;
using SIMPLEAPI_Instructor.DTO;

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
        builder.Services.AddScoped<IInstructor, InstructorEF>();

        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapGet("api/v1/Instructors", (IInstructor instructorData) =>
        {
            var instructors = instructorData.GetAllInstructors();
            var instructorDTOs = instructors.Select(i => new InstructorDTO
            {
                InstructorId = i.InstructorId,
                InstructorName = i.InstructorName,
                InstructorEmail = i.InstructorEmail,
                InstructorPhone = i.InstructorPhone,
                InstructorAddress = i.InstructorAddress,
                InstructorCity = i.InstructorCity,
                Course = i.Course != null ? new CourseDTO
                {
                    CourseId = i.Course.CourseId,
                    CourseName = i.Course.CourseName,
                    CourseDescription = i.Course.CourseDescription,
                    Duration = i.Course.Duration,
                    Category = i.Course.Category != null ? new CategoryDTO
                    {
                        CategoryId = i.Course.Category.CategoryId,
                        CategoryName = i.Course.Category.CategoryName
                    } : null
                } : null
            }).ToList();
            return instructorDTOs;
        });

        app.MapGet("api/v1/Instructors/{id}", (IInstructor instructorData, int id) =>
        {
            var instructor = instructorData.GetInstructorsByID(id);
            if (instructor is null)
            {
                return Results.NotFound();
            }

            var instructorDTO = new InstructorDTO
            {
                InstructorId = instructor.InstructorId,
                InstructorName = instructor.InstructorName,
                InstructorEmail = instructor.InstructorEmail,
                InstructorPhone = instructor.InstructorPhone,
                InstructorAddress = instructor.InstructorAddress,
                InstructorCity = instructor.InstructorCity,
                Course = instructor.Course != null ? new CourseDTO
                {
                    CourseId = instructor.Course.CourseId,
                    CourseName = instructor.Course.CourseName,
                    CourseDescription = instructor.Course.CourseDescription,
                    Duration = instructor.Course.Duration,
                    Category = instructor.Course.Category != null ? new CategoryDTO
                    {
                        CategoryId = instructor.Course.Category.CategoryId,
                        CategoryName = instructor.Course.Category.CategoryName
                    } : null
                } : null
            };
            return Results.Ok(instructorDTO);
        });

        app.MapPut("api/v1/Instructors/{id}", (IInstructor instructorData, int id, InstructorDTO updateDTO) =>
        {
            var existingInstructor = instructorData.GetInstructorsByID(id);
            if (existingInstructor is null)
            {
                return Results.NotFound();
            }

            existingInstructor.InstructorName = updateDTO.InstructorName;
            existingInstructor.InstructorEmail = updateDTO.InstructorEmail;
            existingInstructor.InstructorPhone = updateDTO.InstructorPhone;
            existingInstructor.InstructorAddress = updateDTO.InstructorAddress;
            existingInstructor.InstructorCity = updateDTO.InstructorCity;
            existingInstructor.CourseId = updateDTO.CourseId;

            var updated = instructorData.UpdateInstructors(existingInstructor);
            var updatedWithCourse = instructorData.GetInstructorsByID(updated.InstructorId);

            var instructorDTO = new InstructorDTO
            {
                InstructorId = updatedWithCourse.InstructorId,
                InstructorName = updatedWithCourse.InstructorName,
                InstructorEmail = updatedWithCourse.InstructorEmail,
                InstructorPhone = updatedWithCourse.InstructorPhone,
                InstructorAddress = updatedWithCourse.InstructorAddress,
                InstructorCity = updatedWithCourse.InstructorCity,
                Course = updatedWithCourse.Course != null ? new CourseDTO
                {
                    CourseId = updatedWithCourse.Course.CourseId,
                    CourseName = updatedWithCourse.Course.CourseName,
                    CourseDescription = updatedWithCourse.Course.CourseDescription,
                    Duration = updatedWithCourse.Course.Duration,
                    Category = updatedWithCourse.Course.Category != null ? new CategoryDTO
                    {
                        CategoryId = updatedWithCourse.Course.Category.CategoryId,
                        CategoryName = updatedWithCourse.Course.Category.CategoryName
                    } : null
                } : null
            };

            return Results.Ok(instructorDTO);
        });

        app.MapPost("api/v1/Instructors", (IInstructor instructorData, InstructorAddDTO dto) =>
        {
            var newInstructor = new Instructors
            {
                InstructorName = dto.InstructorName,
                InstructorEmail = dto.InstructorEmail,
                InstructorPhone = dto.InstructorPhone,
                InstructorAddress = dto.InstructorAddress,
                InstructorCity = dto.InstructorCity,
                CourseId = dto.CourseId
            };

            var added = instructorData.AddInstructors(newInstructor);

            var addedWithCourse = instructorData.GetInstructorsByID(added.InstructorId);

            var resultDTO = new InstructorDTO
            {
                InstructorId = addedWithCourse.InstructorId,
                InstructorName = addedWithCourse.InstructorName,
                InstructorEmail = addedWithCourse.InstructorEmail,
                InstructorPhone = addedWithCourse.InstructorPhone,
                InstructorAddress = addedWithCourse.InstructorAddress,
                InstructorCity = addedWithCourse.InstructorCity,
                Course = addedWithCourse.Course != null ? new CourseDTO
                {
                    CourseId = addedWithCourse.Course.CourseId,
                    CourseName = addedWithCourse.Course.CourseName,
                    CourseDescription = addedWithCourse.Course.CourseDescription,
                    Duration = addedWithCourse.Course.Duration,
                    Category = addedWithCourse.Course.Category != null ? new CategoryDTO
                    {
                        CategoryId = addedWithCourse.Course.Category.CategoryId,
                        CategoryName = addedWithCourse.Course.Category.CategoryName
                    } : null
                } : null
            };

            return Results.Created($"/api/v1/Instructors/{resultDTO.InstructorId}", resultDTO);
        });

        app.MapDelete("api/v1/Instructors/{id}", (IInstructor instructorData, int id) =>
        {
            var instructor = instructorData.GetInstructorsByID(id);
            if (instructor is null)
            {
                return Results.NotFound();
            }

            instructorData.DeleteInstructors(id);
            return Results.NoContent();
        });

        /*app.MapGet("api/v1/Courses", (ICourse courseData) =>
        {
            var courses = courseData.GetAllCourses();
            var courseDTOs = courses.Select(c => new CourseDTO
            {
                CourseId = c.CourseId,
                CourseName = c.CourseName,
                CourseDescription = c.CourseDescription,
                Duration = c.Duration,
                Category = new CategoryDTO
                {
                    CategoryId = c.Category.CategoryId,
                   CategoryName = c.Category.CategoryName
                }
            }).ToList();
            return courseDTOs;
        });

        app.MapGet("api/v1/Courses/{id}", (ICourse courseData, int id) =>
        {
            var course = courseData.GetCourseByID(id);
            var courseDTO = new CourseDTO
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                Duration = course.Duration,
                Category = new CategoryDTO
                {
                    CategoryId = course.Category.CategoryId,
                    CategoryName = course.Category.CategoryName
                }
            };
            return courseDTO is not null ? Results.Ok(courseDTO) : Results.NotFound();
        });

        app.MapPut("api/v1/Courses/{id}", (ICourse courseData, int id, CourseUpdateDTO updateDTO) =>
        {
            var existingCourse = courseData.GetCourseByID(id);
            if (existingCourse is null)
            {
                return Results.NotFound();
            }

            existingCourse.CourseName = updateDTO.CourseName;
            existingCourse.CourseDescription = updateDTO.CourseDescription;
            existingCourse.Duration = updateDTO.Duration;
            existingCourse.CategoryId = updateDTO.CategoryId;

            var updated = courseData.UpdateCourse(existingCourse);


            var courseDTO = new CourseDTO
            {
                CourseId = updated.CourseId,
                CourseName = updated.CourseName,
                CourseDescription = updated.CourseDescription,
                Duration = updated.Duration,
                Category = updated.Category != null
                ? new CategoryDTO
                {
                    CategoryId = updated.Category.CategoryId,
                    CategoryName = updated.Category.CategoryName
                }
                : null
            };

            return Results.Ok(courseDTO);
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
        });*/

        app.Run();
    }
}
