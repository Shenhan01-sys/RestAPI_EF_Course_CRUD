using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SIMPLEAPI_Instructor.interfaces;
using SIMPLEAPI_Instructor.models;

namespace SIMPLEAPI_Instructor.data
{
    public class CourseEF : ICourse
    {
        private readonly ApplicationDbContext _context;

        public CourseEF(ApplicationDbContext context)
        {
            _context = context;
        }

        // Implementasi GetCourses
        public IEnumerable<ViewCourse_Categories> GetCourses()
        {
            /*return _context.Courses.Include(c => c.Category).Select(c => new ViewCourse_Categories
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    Duration = c.Duration,
                    CategoryId = c.CategoryId,
                    CategoryName = c.Category != null ? c.Category.CategoryName : string.Empty // Pastikan CategoryName ada di model Category})
                })
                .ToList();*/
            var ViewCourse_Categories = from c in _context.ViewCourse_Categories
                          orderby c.CourseName descending
                          select c;
            return ViewCourse_Categories;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            var courses = from c in _context.Courses.Include(c => c.Category)
                          orderby c.CourseName descending
                          select c;
            return courses;
        }

        public Course GetCourseByID(int CourseId)
        {
            /*var course = _context.Courses.Include(c => c.Category).FirstOrDefault(c => c.CourseId == CourseId);

            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            return new ViewCourse_Categories
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                Duration = course.Duration,
                CategoryId = course.CategoryId,
                CategoryName = course.Category != null ? course.Category.CategoryName : string.Empty // Pastikan CategoryName ada di model Category};
            };*/
            var course = (from c in _context.Courses.Include(c => c.Category)
                                         where c.CourseId == CourseId
                                         select c).FirstOrDefault();
            if (course == null)
            {
                throw new Exception("Course not found woiiiiiiiiiiiii.");
            }
            return course;
        }

        public Course UpdateCourse(Course UpdateCourse)
        {
            var course = _context.Courses.Include(c => c.Category).FirstOrDefault(c => c.CourseId == UpdateCourse.CourseId);
            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            try
            {
                course.CourseName = UpdateCourse.CourseName;
                course.CourseDescription = UpdateCourse.CourseDescription;
                course.Duration = UpdateCourse.Duration;
                course.CategoryId = UpdateCourse.CategoryId;
                _context.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating course: " + ex.Message);
            }
        }

        // Implementasi AddCourse
        public Course AddCourse(Course course)
        {
            try
            {
                _context.Courses.Add(course);
                _context.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding course: " + ex.Message);
            }
        }

        // Implementasi DeleteCourse
        public Course DeleteCourse(int CourseId)
        {
            var course = _context.Courses.FirstOrDefault(c => c.CourseId == CourseId);
            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            try
            {
                _context.Courses.Remove(course);
                _context.SaveChanges();
                return course;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting course: " + ex.Message);
            }
        }
    }
}