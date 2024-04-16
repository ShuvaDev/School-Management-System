using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class EditSubject
    {
        private static SMSDbContext _context { get; set; }
        private static string _currentSubject { get; set; }
        private static string _newSubject { get; set; }
        public static void EditSubjectPanel()
        {
            _context = new SMSDbContext();
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Provide information to edit subject : ");
                    Console.Write("Current subject name : ");
                    _currentSubject = Console.ReadLine();
                    Console.Write("New subject name : ");
                    _newSubject = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_currentSubject) || string.IsNullOrWhiteSpace(_newSubject))
                    {
                        Console.Clear();
                        Console.WriteLine("Subject Name Can't be Empty!");
                    }
                    else
                    {
                        int updateStatus = UpdateSubjectName();
                        Console.Clear();
                        if (updateStatus == 1)
                        {
                            Console.WriteLine($"{_currentSubject} Updated to {_newSubject} Successfully!");
                            ViewSubject.ViewSubjectPanel();
                        }
                        else if (updateStatus == 0)
                        {
                            Console.WriteLine($"{_currentSubject} Not Exists!");
                        }
                        else
                        {
                            Console.WriteLine($"{_newSubject} Already Exists!");
                        }
                    }
                } while (true);
            }
            else
            {
                Console.WriteLine("Access Denied");
            }
        }
        public static int UpdateSubjectName()
        {
            Subject CurrentSubject = _context.Subjects.Where(x => x.SubjectName == _currentSubject).FirstOrDefault();
            if (CurrentSubject == null) return 0;
            else
            {
                Subject NewSubject = _context.Subjects.Where(x => x.SubjectName == _newSubject).FirstOrDefault();
                if (NewSubject == null)
                {
                    CurrentSubject.SubjectName = _newSubject;
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
