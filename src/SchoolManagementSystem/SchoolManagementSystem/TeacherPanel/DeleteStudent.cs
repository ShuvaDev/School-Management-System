using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class DeleteStudent
    {
        private static string _studentName { get; set; }
        public static void DeleteStudentPanel(int classId, string className)
        {
            do
            {
                Console.WriteLine($"Provide information to delete a student of  {className}: ");
                Console.Write("Student Name : ");
                _studentName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(_studentName))
                {
                    Console.Clear();
                    Console.WriteLine("Student Name Can't be Empty!");
                }
                else
                {
                    if (DeleteAStudent(classId))
                    {
                        Console.Clear();
                        Console.WriteLine($"{_studentName} Deleted Successfully!");
                        ViewStudent.ViewStudentPanel(className) ;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"The name {_studentName} Not Exists in {className}!");
                    }
                }
            } while (true);
        }
        private static bool DeleteAStudent(int classId)
        {
            SMSDbContext context = new SMSDbContext();
            Student student = context.Students.Where(x => x.ClassId == classId && x.StudentName == _studentName).FirstOrDefault();

            if (student == null) return false;
            else
            {
                context.Students.Remove(student);
                context.SaveChanges();
                return true;
            }
        }
    }
}
