using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SIMPLEAPI_Instructor.models;
using Microsoft.Data.SqlClient;

namespace SIMPLEAPI_Instructor.data
{
    public class InstructorADO : IInstructor
    {
        private readonly IConfiguration _configuration;
        private string Connect = string.Empty;
        public InstructorADO(IConfiguration configuration)
        {
            _configuration = configuration;
            Connect = _configuration.GetConnectionString("DefaultConnection");
        }

        public Instructor AddInstructor(Instructor instructor)
        {
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                string query = @"INSERT INTO Instructors_RESTAPI (InstructorName, InstructorPhone, InstructorEmail, InstructorAddress, InstructorCity)
                                 VALUES (@InstructorName, @InstructorPhone, @InstructorEmail, @InstructorAddress, @InstructorCity);";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@InstructorName", instructor.InstructorName);
                cmd.Parameters.AddWithValue("@InstructorPhone", instructor.InstructorPhone);
                cmd.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmail);
                cmd.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddress);
                cmd.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCity);
                conn.Open();
                instructor.InstructorId = (int)cmd.ExecuteScalar();
                conn.Close();
            }
            return instructor;
        }

        public void DeleteInstructor(int id)
        {
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                string query = @"DELETE FROM Instructors_RESTAPI WHERE InstructorId = @InstructorId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@InstructorId", id);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public Instructor GetInstructor(int id)
        {
            Instructor instructor = null;
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                string query = @"SELECT * FROM Instructors_RESTAPI WHERE InstructorId = @InstructorId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@InstructorId", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    instructor = new Instructor
                    {
                        InstructorId = Convert.ToInt32(reader["InstructorId"]),
                        InstructorName = reader["InstructorName"].ToString(),
                        InstructorPhone = reader["InstructorPhone"].ToString(),
                        InstructorEmail = reader["InstructorEmail"].ToString(),
                        InstructorAddress = reader["InstructorAddress"].ToString(),
                        InstructorCity = reader["InstructorCity"].ToString()
                    };
                }
                reader.Close();
                conn.Close();
            }
            return instructor;
        }

        public List<Instructor> GetInstructors()
        {
            List<Instructor> Instructors = new List<Instructor>();
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                string query = @"SELECT * FROM Instructors_RESTAPI";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Instructor instructor = new()
                        {
                            InstructorId = Convert.ToInt32(reader["InstructorId"]),
                            InstructorName = reader["InstructorName"].ToString(),
                            InstructorPhone = reader["InstructorPhone"].ToString(),
                            InstructorEmail = reader["InstructorEmail"].ToString(),
                            InstructorAddress = reader["InstructorAddress"].ToString(),
                            InstructorCity = reader["InstructorCity"].ToString()
                        };
                        Instructors.Add(instructor);
                    }
                }
                reader.Close();
                conn.Close();
            }
            return Instructors;
        }

        public Instructor UpdateInstructor(Instructor instructor)
        {
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                string query = @"UPDATE Instructors_RESTAPI
                                 SET InstructorName = @InstructorName, InstructorPhone = @InstructorPhone, InstructorEmail = @InstructorEmail, 
                                     InstructorAddress = @InstructorAddress, InstructorCity = @InstructorCity
                                 WHERE InstructorId = @InstructorId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@InstructorId", instructor.InstructorId);
                cmd.Parameters.AddWithValue("@InstructorName", instructor.InstructorName);
                cmd.Parameters.AddWithValue("@InstructorPhone", instructor.InstructorPhone);
                cmd.Parameters.AddWithValue("@InstructorEmail", instructor.InstructorEmail);
                cmd.Parameters.AddWithValue("@InstructorAddress", instructor.InstructorAddress);
                cmd.Parameters.AddWithValue("@InstructorCity", instructor.InstructorCity);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return instructor;
        }
    }
}