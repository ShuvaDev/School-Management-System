using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Entities;

namespace SchoolManagementSystem.AdminPanel
{
    public static class DeleteTeacher
    {
        private static string _teacherName { get; set; }
        public static void DeleteTeacherPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Provide information to delete a teacher : ");
                    Console.Write("Teacher Name : ");
                    _teacherName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(_teacherName))
                    {
                        Console.Clear();
                        Console.WriteLine("Teacher Name Can't be Empty!");
                    }
                    else
                    {
                        if (DeleteATeacher())
                        {
                            Console.Clear();
                            Console.WriteLine("Teacher Deleted Successfully!");
                            ViewTeacher.ViewTeacherPanel();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Teacher Name Not Exists!");
                        }
                    }
                } while (true);
            }
        }
        private static bool DeleteATeacher()
        {
            SMSDbContext context = new SMSDbContext();
            User teacher = context.Users.Where(x => x.UserName == _teacherName).FirstOrDefault();
            if (teacher == null) return false;
            else
            {
                int deletedTeacherId = teacher.UserId;

                Teacher? t = context.Teachers.Where(t => t.TeacherId == deletedTeacherId)
                    .Include(t => t.Subjects)
                    .Include(t => t.TeacherEnrollments)
                    .FirstOrDefault();
                context.Users.Remove(teacher);

                if (t != null)
                {
                    context.Teachers.Remove(t);
                }

                context.SaveChanges();
                return true;
            }
        }
    }
}
