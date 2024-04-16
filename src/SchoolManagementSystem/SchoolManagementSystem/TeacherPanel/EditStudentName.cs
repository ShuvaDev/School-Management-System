using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class EditStudentName
    {
        private static SMSDbContext _context { get; set; }
        private static string _currentName { get; set; }
        private static string _newName { get; set; }
        public static void EditStudentNamePanel(int classId, string className)
        {
            _context = new SMSDbContext();
            do
            {
                Console.WriteLine("Provide information to edit student : ");
                Console.Write("Current student name : ");
                _currentName = Console.ReadLine();
                Console.Write($"New name of {_currentName}: ");
                _newName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(_currentName) || string.IsNullOrWhiteSpace(_newName))
                {
                    Console.Clear();
                    Console.WriteLine("Student Name Can't be Empty!");
                }
                else
                {
                    int updateStatus = UpdateStudentName(classId);
                    Console.Clear();
                    if (updateStatus == 1)
                    {
                        Console.Clear();
                        Console.WriteLine($"{_currentName} Updated to {_newName} Successfully!");
                        ViewStudent.ViewStudentPanel(className);
                    }
                    else if (updateStatus == 0)
                    {
                        Console.Clear();
                        Console.WriteLine($"{_currentName} Not Exists in {className}!");
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"{_currentName} Already Exists in {className}!");
                    }
                }
            } while (true);
        }
        public static int UpdateStudentName(int classId)
        {
            Student currentStudent = _context.Students.Where(x => x.StudentName == _currentName && x.ClassId == classId).FirstOrDefault();
            if (currentStudent == null) return 0;
            else
            {
                Student newStudent = _context.Students.Where(x => x.StudentName == _newName && x.ClassId == classId).FirstOrDefault();
                if (newStudent == null)
                {
                    currentStudent.StudentName = _newName;
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
