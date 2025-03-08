using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIMPLEAPI_Instructor.models;

namespace SIMPLEAPI_Instructor.data
{
    public class INstructorDataAccessLayer : IInstructor
    {
        private List<Instructor> instructors = new List<Instructor>();
        public INstructorDataAccessLayer()
        {
    instructors.Add(new Instructor
    {
        InstructorId = 1,
        InstructorName = "John Doe",
        InstructorEmail = "John@gamil.com",
        InstructorAddress = "123 Main St",
        InstructorPhone = "123-456-7890",
    });
    instructors.Add(new Instructor
    {
        InstructorId = 2,
        InstructorName = "Jane Doe",
        InstructorEmail = "Jane@gmail.com",
        InstructorAddress = "123 Main St",
        InstructorPhone = "123-456-7890",
    });
    instructors.Add(new Instructor
    {
        InstructorId = 3,
        InstructorName = "John Smith",
        InstructorEmail = "Smith@gmail.com",
        InstructorAddress = "123 Main St",
        InstructorPhone = "123-456-7890",
    });

}

        public Instructor AddInstructor(Instructor instructor)
        {
            throw new NotImplementedException();
        }

        public void DeleteInstructor(int id)
        {
            throw new NotImplementedException();
        }

        public Instructor GetInstructor(int id)
        {
            throw new NotImplementedException();
        }

        public List<Instructor> GetInstructors()
        {
            throw new NotImplementedException();
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            throw new NotImplementedException();
        }
    }
