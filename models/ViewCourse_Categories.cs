using System;

namespace SIMPLEAPI_Instructor.models
{
    public class ViewCourse_Categories
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty; // Nilai default
        public string CourseDescription { get; set; } = string.Empty; // Nilai default
        public int Duration { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty; // Nilai default
    }
}