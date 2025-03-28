using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIMPLEAPI_Instructor.models;

namespace SIMPLEAPI_Instructor.interfaces
{
    public interface ICourse
    {
        public IEnumerable<ViewCourse_Categories> GetCourses();
        public ViewCourse_Categories GetCourseByID(int CourseId);
        public Course AddCourse(Course course);
        public Course DeleteCourse(int courseid); 
    }   
}