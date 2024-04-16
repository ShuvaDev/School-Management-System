using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class AssignTeacher
    {
        private static string _teacherName { get; set; }
        private static string _subjectName { get; set; }
        public static void AssignTeacherPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Provide information to assign a subject to class : ");
                    Console.Write("Teacher Name : ");
                    _teacherName = Console.ReadLine();
                    Console.Write("Subject Name : ");
                    _subjectName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_teacherName) || string.IsNullOrWhiteSpace(_subjectName))
                    {
                        Console.Clear();
                        Console.WriteLine("Teacher Name or Subject Name Can't be Empty!");
                    }
                    else
                    {
                        int AssignedStatus = AssignTeacherToSubject();
                        if (AssignedStatus == 4)
                        {
                            Console.Clear();
                            Console.WriteLine($"{_teacherName} is Assigned to {_subjectName}!");
                            ViewSubject.ViewSubjectPanel();
                        }
                        else if (AssignedStatus == 2)
                        {
                            Console.Clear();
                            Console.WriteLine($"{_subjectName} is Already Assigned to a Teacher!");
                        }
                        else if (AssignedStatus == 3)
                        {
                            Console.Clear();
                            Console.WriteLine($"{_subjectName} is not Assigned in any Class!");
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Subject Name or Teacher Name Not Exists!");
                        }
                    }
                } while (true);
            }
            else
            {
                Console.WriteLine("Access Denied");
            }
        }
        private static int AssignTeacherToSubject()
        {
            using (SMSDbContext context = new SMSDbContext())
            {
                Subject subject = context.Subjects.Where(x => x.SubjectName == _subjectName).FirstOrDefault();
                User user = context.Users.Where(x => x.UserName == _teacherName && x.UserType == false).FirstOrDefault();

                if (subject == null || user == null) return 1;
                else if (subject.TeacherId != null) return 2;
                else if (subject.ClassId == null) return 3;
                else
                {
                    int teacherId = user.UserId;
                    Teacher? t = context.Teachers.Where(x => x.TeacherId == teacherId)
                        .Include(x => x.Subjects)
                        .Include(x => x.TeacherEnrollments)
                        .FirstOrDefault();
                    if (t == null)
                    {
                        Teacher teacher = new Teacher();

                        teacher.TeacherId = teacherId;
                        teacher.Subjects = new();
                        teacher.TeacherEnrollments = new();

                        teacher.Subjects.Add(subject);

                        TeacherEnrollment te = new TeacherEnrollment();
                        Class Class = context.Classes.Where(x => x.ClassId == subject.ClassId.Value).FirstOrDefault();
                        te.Class = Class;

                        teacher.TeacherEnrollments.Add(te);
                        context.Teachers.Add(teacher);
                        context.SaveChanges();

                    }
                    else
                    {
                        t.Subjects.Add(subject);


                        // Check teacher is already assigned to a class or not
                        int flag = 1;
                        foreach (var TE in t.TeacherEnrollments)
                        {
                            if (TE.ClassId == subject.ClassId.Value)
                            {
                                flag = 0;
                                break;
                            }
                        }

                        if (flag == 1)
                        {
                            TeacherEnrollment te = new TeacherEnrollment();
                            Class Class = context.Classes.Where(x => x.ClassId == subject.ClassId.Value).FirstOrDefault();
                            te.Class = Class;

                            t.TeacherEnrollments.Add(te);
                        }
                        context.SaveChanges();
                    }
                    return 4;
                }

            }
        }
    }
}
