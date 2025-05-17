using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SIMPLEAPI_Instructor.models
{
    [Keyless]
    public class ViewInstructor_Courses
    {
        public int InstructorId { get; set; }  // Nilai default
        public string InstructorName { get; set; } = string.Empty;
        public string InstructorEmail { get; set; } = string.Empty;
        public string InstructorPhone { get; set; } = string.Empty;
        public string InstructorAddress { get; set; } = string.Empty;
        public string InstructorCity { get; set; } = string.Empty;
        public int CourseId { get; set; } // Nilai default
        public string CourseName { get; set; } = string.Empty;        
        public int CategoryId {get; set;}
    }
}