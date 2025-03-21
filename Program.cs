using SIMPLEAPI_Instructor.data;
using SIMPLEAPI_Instructor.models;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //DI
        builder.Services.AddSingleton<IInstructor, InstructorADO>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        app.MapGet("api/v1/Instructors", (IInstructor instructorData) =>
        {
            var instructors = instructorData.GetInstructors();
            return instructors;
        });

        app.MapGet("api/v1/Instructors/{id}", (IInstructor instructorData, int id) =>
        {
            var instructor = instructorData.GetInstructor(id);
            return instructor is not null ? Results.Ok(instructor) : Results.NotFound();
        });

        app.MapPost("api/v1/Instructors", (IInstructor instructorData, Instructor instructor) =>
        {
            instructorData.AddInstructor(instructor);
            return Results.Created($"/api/v1/Instructors/{instructor.InstructorId}", instructor);
        });

        app.MapPut("api/v1/Instructors/{id}", (IInstructor instructorData, int id, Instructor updatedInstructor) =>
        {
            var instructor = instructorData.GetInstructor(id);
            if (instructor is null)
            {
                return Results.NotFound();
            }

            instructorData.UpdateInstructor(updatedInstructor);
            return Results.NoContent();
        });

        app.MapDelete("api/v1/Instructors/{id}", (IInstructor instructorData, int id) =>
        {
            var instructor = instructorData.GetInstructor(id);
            if (instructor is null)
            {
                return Results.NotFound();
            }

            instructorData.DeleteInstructor(id);
            return Results.NoContent();
        });

        app.Run();
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
