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
        public IEnumerable<Course> GetAllCourses();
        public Course GetCourseByID(int CourseId);
        public Course AddCourse(Course course);
        public Course UpdateCourse(Course UpdateCourse);
        public Course DeleteCourse(int courseid); 
    }   
}