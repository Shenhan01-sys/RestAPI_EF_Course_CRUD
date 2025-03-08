using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIMPLEAPI_Instructor.models;

namespace SIMPLEAPI_Instructor.data
{
    public interface IInstructor
    {
        public List<Instructor> GetInstructors();
        public Instructor GetInstructor(int id);
        public Instructor AddInstructor(Instructor instructor);
        public Instructor UpdateInstructor(Instructor instructor);
        public void DeleteInstructor(int id);
    }
}