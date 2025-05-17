using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SIMPLEAPI_Instructor.models;

namespace SIMPLEAPI_Instructor.data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Instructors> Instructors { get; set; }
        public DbSet<ViewCourse_Categories> ViewCourse_Categories { get; set; }
        public DbSet<ViewInstructor_Courses> ViewInstructor_Courses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Instructors>().ToTable("Instructors_RESTAPI");
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ViewCourse_Categories>().HasNoKey();
            modelBuilder.Entity<ViewInstructor_Courses>().HasNoKey();
        }

        // Tambahkan konfigurasi lainnya jika diperlukan
    }
}