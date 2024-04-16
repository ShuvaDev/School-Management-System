using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class AssignSubject
    {
        private static string _className { get; set; }
        private static string _subjectName { get; set; }
        public static void AssignSubjectPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Provide information to assign a subject to class : ");
                    Console.Write("Class Name : ");
                    _className = Console.ReadLine();
                    Console.Write("Subject Name : ");
                    _subjectName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_className) || string.IsNullOrWhiteSpace(_subjectName))
                    {
                        Console.Clear();
                        Console.WriteLine("Subject Name or Class Name Can't be Empty!");
                    }
                    else
                    {
                        int AssignedStatus = AssignSubjectToClass();
                        if (AssignedStatus == 3)
                        {
                            Console.Clear();
                            Console.WriteLine($"{_subjectName} is Assigned to {_className}!");
                            ViewClass.ViewClassPanel();
                        }
                        else if (AssignedStatus == 2)
                        {
                            Console.Clear();
                            Console.WriteLine($"{_subjectName} is Already Assigned to a Class!");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Subject Name or Class Name Not Exists!");
                        }
                    }
                } while (true);
            }
            else
            {
                Console.WriteLine("Access Denied");
            }
        }
        private static int AssignSubjectToClass()
        {
            using (SMSDbContext context = new SMSDbContext())
            {
                Subject Subject = context.Subjects.Where(x => x.SubjectName == _subjectName).FirstOrDefault();
                Class Class = context.Classes.Where(x => x.ClassName == _className).FirstOrDefault();

                if (Subject == null || Class == null) return 1;
                else if (Subject.ClassId != null)
                {
                    return 2;
                }
                else
                {
                    if (Class.Subjects == null) Class.Subjects = new();
                    Class.Subjects.Add(Subject);
                    context.SaveChanges();
                    return 3;
                }

            }
        }
    }
}
