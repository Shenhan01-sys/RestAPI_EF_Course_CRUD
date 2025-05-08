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
            return _context.Courses
                .Include(c => c.Category)
                .Select(c => new ViewCourse_Categories
                {
                    CourseId = c.CourseId,
                    CourseName = c.CourseName,
                    CourseDescription = c.CourseDescription,
                    Duration = c.CourseDuration,
                    CategoryId = c.CategoryId,
                    CategoryName = c.Category.CategoryName // Pastikan CategoryName ada di model Category
                })
                .ToList();
        }

        public ViewCourse_Categories GetCourseByID(int CourseId)
        {
            var course = _context.Courses
                .Include(c => c.Category)
                .FirstOrDefault(c => c.CourseId == CourseId);

            if (course == null)
            {
                throw new Exception("Course not found.");
            }

            return new ViewCourse_Categories
            {
                CourseId = course.CourseId,
                CourseName = course.CourseName,
                CourseDescription = course.CourseDescription,
                Duration = course.CourseDuration,
                CategoryId = course.CategoryId,
                CategoryName = course.Category.CategoryName // Pastikan CategoryName ada di model Category
            };
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