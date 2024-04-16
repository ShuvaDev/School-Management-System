using SchoolManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.AdminPanel
{
    public static class CreateTeacher
    {
        private static string TeacherName { get; set; }
        private static string Password { get; set; }
        public static void CreateTeacherPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Please provide following information to create a new teacher : ");
                    Console.Write("Teacher username : ");
                    TeacherName = Console.ReadLine();
                    Console.Write("Teacher password : ");
                    Password = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(TeacherName) || string.IsNullOrWhiteSpace(Password))
                    {
                        Console.Clear();
                        Console.WriteLine("User Name or Password Can't be Empty!");
                    }
                    else
                    {
                        if (InsertTeacher() == 0)
                        {
                            Console.Clear();
                            Console.WriteLine($"{TeacherName} Already Exists!");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("New Teacher Created Successfully!");
                            AdminDashboard.AdminDashboardPanel();
                        }
                    }
                } while (true);
            }
            else
            {
                Console.WriteLine("Access Denied!");
            }
        }
        private static int InsertTeacher()
        {
            SMSDbContext context = new SMSDbContext();
            User user = context.Users.Where(x => x.UserName == TeacherName && x.UserType == false).FirstOrDefault();
            if (user != null)
            {
                return 0;
            }
            context.Users.Add(new User() { UserName = TeacherName, Password = Password, UserType = false });
            context.SaveChanges();
            return 1;
        }
    }
}
