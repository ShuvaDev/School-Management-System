using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace SchoolManagementSystem.AdminPanel
{
    public static class CreateClass
    {
        private static string ClassName { get; set; }
        public static void CreateClassPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Please provide following information to create a new class : ");
                    Console.Write("Class Name : ");
                    ClassName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(ClassName))
                    {
                        Console.Clear();
                        Console.WriteLine("Class Name Can't be Empty!");
                    }
                    else
                    {

                        if (InsertClass())
                        {
                            Console.Clear();
                            Console.WriteLine("New Class Created Successfully!");
                            AdminDashboard.AdminDashboardPanel();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Class Name Already Exists!");
                        }
                    }
                } while (true);
            }
            else
            {
                Console.WriteLine("Access Denied!");
            }
        }
        private static bool InsertClass()
        {
            SMSDbContext context = new SMSDbContext();
            Class Class = context.Classes.Where(x => x.ClassName == ClassName).FirstOrDefault();
            if (Class != null) return false;
            else
            {
                context.Classes.Add(new Class() { ClassName = ClassName });
                context.SaveChanges();
                return true;
            }
        }
    }
}
