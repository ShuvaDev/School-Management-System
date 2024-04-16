using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class EditTeacher
    {
        private static SMSDbContext _context { get; set; }
        private static string _oldName { get; set; }
        private static string _newName { get; set; }
        public static void EditTeacherPanel()
        {
            _context = new SMSDbContext();
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Provide information to edit teacher name : ");
                    Console.Write("Old Name : ");
                    _oldName = Console.ReadLine();
                    Console.Write("New Name : ");
                    _newName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_oldName) || string.IsNullOrWhiteSpace(_newName))
                    {
                        Console.Clear();
                        Console.WriteLine("Teacher Name Can't be Empty!");
                    }
                    else
                    {
                        int updateStatus = UpdateTeacherName();
                        Console.Clear();
                        if (updateStatus == 1)
                        {
                            Console.WriteLine($"{_oldName} Updated to {_newName} Successfully!");
                            ViewTeacher.ViewTeacherPanel();
                        }
                        else if (updateStatus == 0)
                        {
                            Console.WriteLine($"Teacher Name '{_oldName}' Not Exists!");
                        }
                        else
                        {
                            Console.WriteLine($"{_newName} is Already Assigned as a Teacher!");
                        }
                    }
                } while (true);
            }
            else
            {
                Console.WriteLine("Access Denied");
            }
        }
        public static int UpdateTeacherName()
        {
            User oldTeacher = _context.Users.Where(x => x.UserName == _oldName && x.UserType == false).FirstOrDefault();
            if (oldTeacher == null) return 0;
            else
            {
                User newTeacher = _context.Users.Where(x => x.UserName == _newName).FirstOrDefault();
                if (newTeacher == null)
                {
                    oldTeacher.UserName = _newName;
                    _context.SaveChanges();
                    return 1;
                }
                else
                {
                    return 2;
                }
            }
        }
    }
}
