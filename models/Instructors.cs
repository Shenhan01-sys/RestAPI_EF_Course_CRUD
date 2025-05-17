using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace SIMPLEAPI_Instructor.models
{
    public class Instructors
    {
        [Key]
        public int InstructorId { get; set; } // Nilai default
        public string InstructorName { get; set; }  = string.Empty; // Nilai default
        public string InstructorEmail { get; set; }  = string.Empty; // Nilai default
        public string InstructorPhone { get; set; }  = string.Empty; // Nilai default
        public string InstructorAddress { get; set; }  = string.Empty; // Nilai default
        public string InstructorCity { get; set; }  = string.Empty; // Nilai default
        public int CourseId { get; set; } // Nilai default
        public Course Course { get; set; } = null!; // Nilai default
        }
}