using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class DeleteClass
    {
        private static string _className { get; set; }
        public static void DeleteClassPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("[If you delete a class, all subjects of that class will become unassigned, but not deleted.]");
                    Console.WriteLine("[All students of the deleted class will be deleted.]");
                    Console.WriteLine("\nProvide information to delete a class : ");
                    Console.Write("Class Name : ");
                    _className = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_className))
                    {
                        Console.Clear();
                        Console.WriteLine("Class Name Can't be Empty!");
                    }
                    else
                    {
                        if (DeleteAClass())
                        {
                            Console.Clear();
                            Console.WriteLine("Class Deleted Successfully!");
                            ViewClass.ViewClassPanel();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Class Name Not Exists!");
                        }
                    }
                } while (true);
            }
        }
        private static bool DeleteAClass()
        {
            SMSDbContext context = new SMSDbContext();
            Class Class = context.Classes.Where(x => x.ClassName == _className).FirstOrDefault();
            if (Class == null) return false;
            else
            {
                int deletedClassId = Class.ClassId;
                // Iterate all subject which has same class id and set it to null
                List<Subject> subjects = context.Subjects.Where(x => x.ClassId == deletedClassId).ToList();
                foreach (var subject in subjects)
                {
                    subject.ClassId = null;
                    subject.TeacherId = null;
                }
                // Iterate all teacher enrollment and delete it
                Class.TeacherEnrollments = new List<TeacherEnrollment>();


                context.Classes.Remove(Class);
                context.SaveChanges();
                return true;
            }
        }
    }
}
