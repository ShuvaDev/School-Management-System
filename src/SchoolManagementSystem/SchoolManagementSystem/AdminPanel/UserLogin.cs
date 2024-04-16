using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.TeacherPanle;

namespace SchoolManagementSystem.AdminPanel
{
    public static class UserLogin
    {
        public static string UserName { get; set; }
        private static string Password { get; set; }
        private static bool IsInvalid { get; set; }
        public static bool UserType { get; set; }
        public static void UserLoginPanel()
        {
            IsInvalid = false;
            do
            {
                if (IsInvalid)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid User Name or Password! Please Try Again.");
                }

                Console.WriteLine("Please Login:");
                Console.Write("Username: ");
                UserName = Console.ReadLine();
                Console.Write("Password: ");
                Password = Console.ReadLine();

            } while (!IsUserExists());

            Console.Clear();
            if (UserType == true)
                AdminDashboard.AdminDashboardPanel();
            else
                TeacherDashboard.TeacherDashboardPanel();
        }
        private static bool IsUserExists()
        {
            SMSDbContext context = new SMSDbContext();
            User? user = context.Users.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
            if (user != null)
            {
                UserType = user.UserType;
                return true;
            }
            else
            {
                IsInvalid = true;
                return false;
            }
        }
    }
}
