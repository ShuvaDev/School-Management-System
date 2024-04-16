using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class CreateSubject
    {
        private static string SubjectName { get; set; }
        public static void CreateSubjectPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Please provide following information to create a new subject : ");
                    Console.Write("Subject Name : ");
                    SubjectName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(SubjectName))
                    {
                        Console.Clear();
                        Console.WriteLine("Subject Name Can't be Empty!");
                    }
                    else
                    {
                        if (InsertSubject())
                        {
                            Console.Clear();
                            Console.WriteLine("New Subject Created Successfully!");
                            AdminDashboard.AdminDashboardPanel();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Subject Name Already Exists!");
                        }
                    }
                } while (true);
            }
        }
        private static bool InsertSubject()
        {
            using (SMSDbContext context = new SMSDbContext())
            {
                Subject subject = context.Subjects.Where(x => x.SubjectName == SubjectName).FirstOrDefault();
                if (subject != null) return false;
                else
                {
                    context.Subjects.Add(new Subject() { SubjectName = SubjectName });
                    context.SaveChanges();
                    return true;
                }

            }
        }
    }
}
