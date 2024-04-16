using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem
{
    internal class SMSDbContext : DbContext
    {
        private readonly string _conn;
        public SMSDbContext()
        {
            _conn = "Data Source=.\\SQLEXPRESS;Initial Catalog=CSharpB16;User ID=csharpb16; Password=123456;TrustServerCertificate=True;";

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_conn);
            }
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User { UserId = 1, UserName = "admin", Password = "12345", UserType = true });

            // One to Many Between Class and Subjects
            modelBuilder.Entity<Class>()
                .HasMany(c => c.Subjects)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassId);

            // One to Many Between Class and Students
            modelBuilder.Entity<Class>()
                .HasMany(c => c.Students)
                .WithOne(s => s.Class)
                .HasForeignKey(s => s.ClassId);

            // Many to Many Between Teachers and Courses
            modelBuilder.Entity<TeacherEnrollment>()
                .HasKey(te => new { te.TeacherId, te.ClassId });

            modelBuilder.Entity<TeacherEnrollment>()
                .HasOne(te => te.Teacher)
                .WithMany(c => c.TeacherEnrollments)
                .HasForeignKey(te => te.TeacherId);

            modelBuilder.Entity<TeacherEnrollment>()
                .HasOne(te => te.Class)
                .WithMany(t => t.TeacherEnrollments)
                .HasForeignKey(te => te.ClassId);

            // One to Many Between Teacher and Subjects

            modelBuilder.Entity<Teacher>()
                .Property(t => t.TeacherId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Teacher>()
                .HasMany(t => t.Subjects)
                .WithOne(s => s.Teacher)
                .HasForeignKey(s => s.TeacherId);

            // One to Many Between Student and Grade
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentId);

            // One to Many Between Subject and Grade
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Subject)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.SubjectId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<TeacherEnrollment> TeacherEnrollments { get; set; }
    }
}
