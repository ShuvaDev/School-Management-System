using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.AdminPanel
{
    public static class ViewTeacher
    {
        public static void ViewTeacherPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    if (ListOfTeachers() == 0)
                    {
                        Console.WriteLine("\n\nThere are no teachers whose are Assigned to a subject! ");
                        int key = 0;
                        do
                        {
                            Console.Write("\nPress 1 for go to dashboard : ");
                            int.TryParse(Console.ReadLine(), out key);
                        } while (key != 1);
                        Console.Clear();
                        AdminDashboard.AdminDashboardPanel();
                    }
                    else
                    {
                        Console.WriteLine("\nWhat you wnat to do?");
                        Console.WriteLine("1) Edit Teacher Name");
                        Console.WriteLine("2) Delete Teacher");
                        Console.WriteLine("3) Remove A Subject From Teacher");
                        Console.WriteLine("4) Back to dashboard");


                        Console.Write("\nInput Your Option : ");
                        int option;
                        int.TryParse(Console.ReadLine(), out option);

                        if (option != 1 && option != 2 && option != 3 && option != 4)
                        {
                            Console.Clear();
                            Console.WriteLine("Invalid Input!\n");
                        }
                        else
                        {
                            Console.Clear();
                            switch (option)
                            {
                                case 1:
                                    EditTeacher.EditTeacherPanel();
                                    break;
                                case 2:
                                    DeleteTeacher.DeleteTeacherPanel();
                                    break;
                                case 3:
                                    RemoveSubject.RemoveSubjectPanel();
                                    break;
                                case 4:
                                    AdminDashboard.AdminDashboardPanel();
                                    break;
                            }
                            break;
                        }
                    }

                } while (true);
            }
            else
            {
                Console.WriteLine("Access Denied!");
            }


        }
        private static int ListOfTeachers()
        {
            SMSDbContext context = new SMSDbContext();
            var teachers = context.Teachers.ToList();

            if (teachers.Count == 0)
            {
                return 0;
            }

            Console.WriteLine("Following Teachers are present in the system : ");
            for (int i = 1; i <= 72; i++) Console.Write("_");
            Console.WriteLine();
            Console.Write("      Teacher Name          Enrolled Subject         Enrolled Class");
            Console.WriteLine();
            for (int i = 1; i <= 72; i++) Console.Write("_");
            Console.WriteLine();


            foreach (var teacher in teachers)
            {
                User user = context.Users.Where(x => x.UserId == teacher.TeacherId).FirstOrDefault();
                var teacherName = user.UserName;
                List<Subject> enrolledSubjects = context.Subjects.Where(s => s.TeacherId == teacher.TeacherId).ToList();
                List<TeacherEnrollment> enrolledClasses = context.TeacherEnrollments.Where(te => te.TeacherId == teacher.TeacherId)
                    .Include(te => te.Class)
                    .ToList();


                string[,] teacherTable = new string[(enrolledSubjects.Count == 0 ? 1 : enrolledSubjects.Count), 3];
                teacherTable[0, 0] = teacherName;
                if (enrolledSubjects.Count == 0) teacherTable[0, 1] = "None";
                else teacherTable[0, 1] = enrolledSubjects[0].SubjectName;
                if (enrolledClasses.Count == 0) teacherTable[0, 2] = "None";
                else teacherTable[0, 2] = enrolledClasses[0].Class.ClassName;

                for (int i = 1; i < enrolledSubjects.Count; i++)
                {
                    teacherTable[i, 0] = "";
                    teacherTable[i, 1] = enrolledSubjects[i].SubjectName;
                    if (i < enrolledClasses.Count)
                        teacherTable[i, 2] = enrolledClasses[i].Class.ClassName;
                    else
                        teacherTable[i, 2] = "";
                }


                for (int i = 0; i < (enrolledSubjects.Count == 0 ? 1 : enrolledSubjects.Count); i++)
                {
                    Console.Write(teacherTable[i, 0]);
                    for (int j = 1; j <= 28 - teacherTable[i, 0].Length; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(teacherTable[i, 1]);
                    for (int j = 1; j <= 24 - teacherTable[i, 1].Length; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.Write(teacherTable[i, 2]);
                    for (int j = 1; j <= 24 - teacherTable[i, 2].Length; j++)
                    {
                        Console.Write(" ");
                    }
                    Console.WriteLine();
                }
                for (int i = 1; i <= 72; i++) Console.Write("_");
                Console.WriteLine();
            }
            return 1;
        }
    }
}
