using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIMPLEAPI_Instructor.interfaces;
using SIMPLEAPI_Instructor.models;
using Microsoft.EntityFrameworkCore;

namespace SIMPLEAPI_Instructor.data
{
    public class InstructorEF : IInstructor
    {
        private readonly ApplicationDbContext _context;

        public InstructorEF(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ViewInstructor_Courses> GetInstructors()
        {
            // Contoh implementasi, sesuaikan dengan kebutuhan
            var _ViewInstructor_Courses = from c in _context.ViewInstructor_Courses
                          orderby c.InstructorName descending
                          select c;
            return _ViewInstructor_Courses;
        }

        public IEnumerable<Instructors> GetAllInstructors()
        {
            var instructors = from c in _context.Instructors
            .Include(c => c.Course).ThenInclude(c => c.Category)
                orderby c.InstructorName descending
                select c;
            return instructors;
        }

        public Instructors GetInstructorsByID(int InstructorId)
        {
            //return _context.Instructors.FirstOrDefault(i => i.InstructorId == InstructorId);
            var instructor = (from c in _context.Instructors
            .Include(c => c.Course).ThenInclude(c => c.Category)
                where c.InstructorId == InstructorId
                select c).FirstOrDefault();
            if (instructor == null)
            {
                throw new Exception("Course not found woiiiiiiiiiiiii.");
            }
            return instructor;
        }

        public Instructors AddInstructors(Instructors instructors)
        {
             try
            {
                _context.Instructors.Add(instructors);
                _context.SaveChanges();
                return instructors;
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding course: " + ex.Message);
            }
        }

        public Instructors UpdateInstructors(Instructors UpdateInstructor)
        {
            var existing = _context.Instructors.FirstOrDefault(i => i.InstructorId == UpdateInstructor.InstructorId);

            if (existing != null)
            {
                existing.InstructorName = UpdateInstructor.InstructorName;
                existing.InstructorEmail = UpdateInstructor.InstructorEmail;
                existing.InstructorPhone = UpdateInstructor.InstructorPhone;
                existing.InstructorAddress = UpdateInstructor.InstructorAddress;
                existing.InstructorCity = UpdateInstructor.InstructorCity;
                existing.CourseId = UpdateInstructor.CourseId;
                // Jika perlu update Course, tambahkan di sini
                _context.SaveChanges();
                
                _context.Entry(existing).Reference(e => e.Course).Load();
                if (existing.Course != null)
                {
                    _context.Entry(existing.Course).Reference(c => c.Category).Load();
                }
                return existing;
            }
            else
            {
                throw new Exception("Instructor not found lollll.");
            }
        }

        public Instructors DeleteInstructors(int InstructorId)
        {
            var instructor = _context.Instructors.FirstOrDefault(i => i.InstructorId == InstructorId);
            if (instructor == null)
            {
                throw new Exception("Instructor not found.");
            }
            try
            {
                _context.Instructors.Remove(instructor);
                _context.SaveChanges();
                return instructor;
            }
            catch (Exception ex)
            {
                throw new Exception("Instructor not found." + ex.Message);
            }
        }
    }
}