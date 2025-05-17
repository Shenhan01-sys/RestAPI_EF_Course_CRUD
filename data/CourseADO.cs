using System;
using System.Collections.Generic;
using SIMPLEAPI_Instructor.interfaces;
using SIMPLEAPI_Instructor.models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SIMPLEAPI_Instructor.data
{
    public class CourseADO : ICourse
    {
        private readonly IConfiguration _configuration;
        private string Connect = string.Empty;
        public CourseADO(IConfiguration configuration)
        {
            _configuration = configuration;
            Connect = _configuration.GetConnectionString("DefaultConnection");
        }

        public IEnumerable<ViewCourse_Categories> GetCourses()
        {
            string query  = @"SELECT     dbo.Courses.CourseId, dbo.Courses.CourseName, dbo.Courses.CourseDescription, dbo.Courses.Duration, dbo.Categories.CategoryId, dbo.Categories.CategoryName
                            FROM         dbo.Categories INNER JOIN
                                         dbo.Courses ON dbo.Categories.CategoryId = dbo.Courses.CategoryId";
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<ViewCourse_Categories> courses = new List<ViewCourse_Categories>();
                    while (reader.Read())
                    {
                        courses.Add(new ViewCourse_Categories
                        {
                            CourseId = reader.GetInt32(0),
                            CourseName = reader.GetString(1),
                            CourseDescription = reader.GetString(2),
                            Duration = reader.GetInt32(3),
                            CategoryId = reader.GetInt32(4),
                            CategoryName = reader.GetString(5)
                        });
                    }
                    return courses;
                }
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            string query  = @"SELECT * FROM Courses";
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    List<Course> courses = new List<Course>();
                    while (reader.Read())
                    {
                        courses.Add(new Course
                        {
                            CourseId = reader.GetInt32(0),
                            CourseName = reader.GetString(1),
                            CourseDescription = reader.GetString(2),
                            Duration = reader.GetInt32(3),
                            CategoryId = reader.GetInt32(4)
                        });
                    }
                    return courses;
                }
            }
        }

        public Course GetCourseByID(int CourseId)
        {
            string query = @"SELECT * FROM Courses WHERE CourseId = @CourseId";
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@CourseId", CourseId);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Course
                        {
                            CourseId = reader.GetInt32(0),
                            CourseName = reader.GetString(1),
                            CourseDescription = reader.GetString(2),
                            Duration = reader.GetInt32(3),
                            CategoryId = reader.GetInt32(4)
                        };
                    }
                    else
                    {
                        throw new Exception("Course not found");
                    }
                }
            }
        }

        public Course UpdateCourse(Course UpdateCourse)
        {   
            throw new NotImplementedException();
        }
        public Course AddCourse(Course course)
        {
            string query = @"INSERT INTO Courses (CourseName, CourseDescription, Duration, CategoryId)
                                VALUES (@CourseName, @CourseDescription, @Duration, @CategoryId); SELECT SCOPE_IDENTITY();";
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("@CourseName", course.CourseName);
                        cmd.Parameters.AddWithValue("@CourseDescription", course.CourseDescription);
                        cmd.Parameters.AddWithValue("@Duration", course.Duration);
                        cmd.Parameters.AddWithValue("@CategoryId", course.CategoryId);

                        conn.Open();
                        int courseid = Convert.ToInt32(cmd.ExecuteScalar());
                        course.CourseId = courseid;
                        return course;
                    }
                    catch (Exception)
                    {
                        throw; // Melempar ulang exception tanpa kehilangan stack trace
                    }
                    finally
                    {
                        cmd.Dispose();
                        conn.Close();
                    }
                }
            }
        }

        public Course DeleteCourse(int CourseId)
        {
            string query = @"DELETE FROM Courses WHERE CourseId = @CourseId";
            using (SqlConnection conn = new SqlConnection(Connect))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Courseid", CourseId);
                    conn.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return new Course { CourseId = CourseId }; // Contoh pengembalian
                    }
                    else
                    {
                        throw new Exception("Course not found");
                    }       
                }
            }
        }
    }   
}