using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class DeleteSubject
    {
        private static string _subjectName { get; set; }
        public static void DeleteSubjectPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Provide information to delete a subject : ");
                    Console.Write("Subject Name : ");
                    _subjectName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_subjectName))
                    {
                        Console.Clear();
                        Console.WriteLine("Subject Name Can't be Empty!");
                    }
                    else
                    {
                        if (DeleteASubject())
                        {
                            Console.Clear();
                            Console.WriteLine($"{_subjectName} Deleted Successfully!");
                            ViewSubject.ViewSubjectPanel();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"{_subjectName} Not Exists!");
                        }
                    }
                } while (true);
            } 
            else
            {
                Console.WriteLine("Access denied!");
            }
        }
        private static bool DeleteASubject()
        {
            SMSDbContext context = new SMSDbContext();
            Subject Subject = context.Subjects.Where(x => x.SubjectName == _subjectName).FirstOrDefault();

            if (Subject == null) return false;
            else
            {
                var classId = Subject.ClassId;
                var teacherId = Subject.TeacherId;

                int totalEnrollment = context.Subjects.Where(x => x.ClassId == classId && x.TeacherId == teacherId).Count();

                if (totalEnrollment == 1)
                {
                    TeacherEnrollment teacherEnrollment = context.TeacherEnrollments.Where(x => x.ClassId == classId).FirstOrDefault();
                    context.TeacherEnrollments.Remove(teacherEnrollment);
                }

                context.Subjects.Remove(Subject);
                context.SaveChanges();
                return true;
            }
        }
    }
}
