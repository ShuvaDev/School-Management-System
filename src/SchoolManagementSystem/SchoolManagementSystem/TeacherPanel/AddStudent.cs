using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.TeacherPanle;

namespace SchoolManagementSystem.AdminPanel
{
    public static class AddStudent
    {
        private static string _className { get; set; }
        private static string _studentName { get; set; }
        public static void AddStudentPanel()
        {
            do
            {
                Console.WriteLine("Provide information to add student : ");
                Console.Write("Class Name : ");
                _className = Console.ReadLine();
                Console.Write("Student Name : ");
                _studentName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(_className) || string.IsNullOrWhiteSpace(_studentName))
                {
                    Console.Clear();
                    Console.WriteLine("Class Name or Student Name Can't be Empty!");
                }
                else
                {
                    int AssignedStatus = AddStudentInClass();
                    if (AssignedStatus == 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"{_className} Not Exists!");
                    }
                    else if (AssignedStatus == 1)
                    {
                        Console.Clear();
                        Console.WriteLine($"{_studentName} Already Added in {_className}!");
                    }
                    else if (AssignedStatus == 2)
                    {
                        Console.Clear();
                        Console.WriteLine($"{_studentName} is Added in {_className}!");
                        TeacherDashboard.TeacherDashboardPanel();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"{UserLogin.UserName} is not a Teacher of {_className}!");
                    }
                }
            } while (true);
        }
        private static int AddStudentInClass()
        {
            using (SMSDbContext context = new SMSDbContext())
            {
                Class? Class = context.Classes.Where(x => x.ClassName == _className)
                    .Include(x => x.TeacherEnrollments)
                    .FirstOrDefault();

                if (Class == null) return 0; 
                Student? Student = context.Students.Where(x => x.StudentName == _studentName && x.ClassId == Class.ClassId).FirstOrDefault();

                if (Student != null) return 1;

                int teacherId = context.Users.Where(x => x.UserName == UserLogin.UserName).FirstOrDefault().UserId;

                int flag = 0;
                foreach(var teacherEnrollment in Class.TeacherEnrollments)
                {
                    if(teacherEnrollment.TeacherId == teacherId)
                    {
                        flag = 1;
                        break;
                    }
                }

                if(flag == 1)
                {
                    Student std = new() { StudentName = _studentName, Class = Class };
                    context.Students.Add(std);
                    context.SaveChanges();
                    return 2;
                }

                return 3;
            }
        }
    }
}
