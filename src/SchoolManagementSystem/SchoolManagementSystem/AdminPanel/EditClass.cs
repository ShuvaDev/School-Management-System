using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class EditClass
    {
        private static SMSDbContext _context { get; set; }
        private static string _currentClass { get; set; }
        private static string _newClass { get; set; }
        public static void EditClassPanel()
        {
            _context = new SMSDbContext();
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Provide information to edit class : ");
                    Console.Write("Current class name : ");
                    _currentClass = Console.ReadLine();
                    Console.Write("New class name : ");
                    _newClass = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_currentClass) || string.IsNullOrWhiteSpace(_newClass))
                    {
                        Console.Clear();
                        Console.WriteLine("Class Name Can't be Empty!");
                    }
                    else
                    {
                        int updateStatus = UpdateClassName();
                        Console.Clear();
                        if (updateStatus == 1)
                        {
                            Console.WriteLine($"{_currentClass} Updated to {_newClass} Successfully!");
                            ViewClass.ViewClassPanel();
                        }
                        else if (updateStatus == 0)
                        {
                            Console.WriteLine($"{_currentClass} Not Exists!");
                        }
                        else
                        {
                            Console.WriteLine($"{_newClass} Already Exists!");
                        }
                    }
                } while (true);
            }
            else
            {
                Console.WriteLine("Access Denied");
            }
        }
        public static int UpdateClassName()
        {
            Class CurrentClass = _context.Classes.Where(x => x.ClassName == _currentClass).FirstOrDefault();
            if (CurrentClass == null) return 0;
            else
            {
                Class NewClass = _context.Classes.Where(x => x.ClassName == _newClass).FirstOrDefault();
                if (NewClass == null)
                {
                    CurrentClass.ClassName = _newClass;
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
