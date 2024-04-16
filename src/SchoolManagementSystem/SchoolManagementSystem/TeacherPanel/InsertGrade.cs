using SchoolManagementSystem.AdminPanel;
using SchoolManagementSystem.TeacherPanle;
using SchoolManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.TeacherPanel
{
    public class InsertGrade
    {
        private static string className { get; set; }
        private static string subjectName { get; set; }
        private static string studentName { get; set; }
        private static string term { get; set; }
        private static double grade { get; set; }
        private static SMSDbContext context;
        public static void InsertGradePanel()
        {
            do
            {
                InputData();

                if (string.IsNullOrWhiteSpace(className) || string.IsNullOrWhiteSpace(subjectName) || string.IsNullOrWhiteSpace(studentName) || string.IsNullOrWhiteSpace(term))
                {
                    Console.WriteLine("Class name or Subject name or Student name or Term can't be empty!");
                }
                else
                {
                    context = new SMSDbContext();
                    // Check inputed data exists or not
                    string error = "";
                    Class Class = context.Classes.Where(x => x.ClassName == className).FirstOrDefault();
                    Subject? subject = null;
                    Student? student = null;
                    if(Class != null)
                    {
                        subject = context.Subjects.Where(x => x.SubjectName == subjectName && x.ClassId == Class.ClassId)
                            .Include(x => x.Grades)
                            .FirstOrDefault();

                        student = context.Students.Where(x => x.StudentName == studentName && x.ClassId == Class.ClassId).FirstOrDefault();
                    }


                    if (Class == null) error = String.Concat(error, $"Class Name '{className}' Not Exists in System!\n");
                    else
                    {
                        if (subject == null) error = String.Concat(error, $"Subject Name '{subjectName}' Not Exists in {className}!\n");
                        if (student == null) error = String.Concat(error, $"Student Name '{studentName}' Not Exists in {className}!\n");

                    }
                    if (!(term == "1st" || term == "mid" || term == "final")) error = String.Concat(error, "Term name must be 1st or mid or final!\n");
                    if (grade < 0 || grade > 5) error = String.Concat(error, "Grade must be between 0.00 to 5.00!\n");

                    if (error.Length > 1)
                    {
                        Console.Clear();
                        Console.WriteLine(error);
                    } 
                    else
                    {
                        int teacherId = context.Users.Where(x => x.UserName == UserLogin.UserName && x.UserType == false).First().UserId;
                        if(subject.TeacherId != teacherId)
                        {
                            Console.Clear();
                            Console.WriteLine($"{UserLogin.UserName} is not a teacher of {subjectName}!");
                            TeacherDashboard.TeacherDashboardPanel();
                        } 
                        else
                        {
                            int studentId = student.StudentId;
                            var stdGrade = subject.Grades.Where(g => g.StudentId == studentId).FirstOrDefault();


                            if (stdGrade == null)
                            {
                                if (term == "1st")
                                {
                                    InsertGradeInDB(subject, student);
                                    Console.Clear();
                                    Console.WriteLine("Grade Inserted Successfully!");
                                    TeacherDashboard.TeacherDashboardPanel();

                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Your must be 1st term grade before mid or final!");
                                }
                            }
                            else
                            {
                                if (term == "mid")
                                {
                                    stdGrade.mid_term = grade;
                                    context.SaveChanges();
                                    Console.Clear();
                                    Console.WriteLine("Grade Inserted Successfully!");
                                    TeacherDashboard.TeacherDashboardPanel();
                                }
                                else if(term == "final" && stdGrade.mid_term != null)
                                {
                                    stdGrade.final_term = grade;
                                    context.SaveChanges();
                                    Console.Clear();
                                    Console.WriteLine("Grade Inserted Successfully!");
                                    TeacherDashboard.TeacherDashboardPanel();
                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("You must be insert mid term grade before final!");
                                }
                            }
                        }
                    }
                }
            } while (true);
        }
        public static void InputData()
        {
            Console.WriteLine("Please provide following information to insert grade : ");
            Console.Write("Class Name : ");
            className = Console.ReadLine();
            Console.Write("Subject Name : ");
            subjectName = Console.ReadLine();
            Console.Write("Student Name : ");
            studentName = Console.ReadLine();
            Console.Write("Term Name (1st, mid, final) : ");
            term = Console.ReadLine();
            Console.Write("Grade (0.00 to 5.00) : ");
            grade = Convert.ToDouble(Console.ReadLine());
        }
        public static void InsertGradeInDB(Subject subject, Student student)
        {
            Grade g = new Grade() { Subject = subject, Student = student };

            if (term == "1st") g.first_term = grade;
            else if (term == "mid") g.mid_term = grade;
            else g.final_term = grade;

            context.Grades.Add(g);
            context.SaveChanges();
        }
    }
}
