using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIMPLEAPI_Instructor.models
{
    public class Course
    {
        public int CourseId {get; set;}
        public string CourseName { get; set; } = string.Empty; // Tambahkan nilai default
        public string CourseDescription { get; set; } = string.Empty; // Tambahkan nilai default
        public int Duration { get; set; } // Tambahkan nilai default
        public int CategoryId {get; set;}
        public Category? Category { get; set; }
    }
}