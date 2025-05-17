using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SIMPLEAPI_Instructor.models
{
    public class Category
    {
        public int CategoryId { get; set; } = 0; // Nilai default
        public string CategoryName { get; set; } = string.Empty; // Nilai default

        public IEnumerable<Course> Courses { get; set; } = new List<Course>(); // Nilai default
    }
}