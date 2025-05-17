using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIMPLEAPI_Instructor.models;

namespace SIMPLEAPI_Instructor.interfaces
{
    public interface IInstructor
    {
        public IEnumerable<ViewInstructor_Courses> GetInstructors();
        public IEnumerable<Instructors> GetAllInstructors();
        public Instructors GetInstructorsByID(int InstructorId);
        public Instructors AddInstructors(Instructors instructors);
        public Instructors UpdateInstructors(Instructors UpdateInstructor);
        public Instructors DeleteInstructors(int InstructorId); 
    }
}