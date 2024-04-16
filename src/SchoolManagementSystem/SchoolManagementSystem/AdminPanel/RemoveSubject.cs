using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class RemoveSubject
    {
        private static string _subjectName { get; set; }
        private static string _teacherName { get; set; }
        public static void RemoveSubjectPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Provide information to remove a subject from teacher : ");
                    Console.Write("Teacher Name : ");
                    _teacherName = Console.ReadLine();
                    Console.Write("Subject Name : ");
                    _subjectName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_subjectName) || string.IsNullOrWhiteSpace(_teacherName))
                    {
                        Console.Clear();
                        Console.WriteLine("Subject Name or Teacher Name Can't be Empty!");
                    }
                    else
                    {
                        if (RemoveASubject())
                        {
                            Console.Clear();
                            Console.WriteLine($"{_subjectName} Removed Successfully!");
                            ViewTeacher.ViewTeacherPanel();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine($"{_subjectName} or {_teacherName} Not Exists!");
                        }
                    }
                } while (true);
            }
        }
        private static bool RemoveASubject()
        {
            SMSDbContext context = new SMSDbContext();
            User teacher = context.Users.Where(x => x.UserName == _teacherName).FirstOrDefault();
            Subject subject = null;
            if (teacher != null)
                subject = context.Subjects.Where(x => x.SubjectName == _subjectName && x.TeacherId == teacher.UserId).FirstOrDefault();

            if (subject == null || teacher == null) return false;
            else
            {
                var teacherId = teacher.UserId;
                var classId = subject.ClassId;


                int totalEnrollment = context.Subjects.Where(x => x.ClassId == classId && x.TeacherId == teacherId).Count();

                if (totalEnrollment == 1)
                {
                    TeacherEnrollment teacherEnrollment = context.TeacherEnrollments.Where(x => x.ClassId == classId).FirstOrDefault();
                    context.TeacherEnrollments.Remove(teacherEnrollment);
                }

                subject.TeacherId = null;
                context.SaveChanges();
                return true;
            }
        }
    }
}
