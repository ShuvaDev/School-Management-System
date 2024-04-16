using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.TeacherPanle;

namespace SchoolManagementSystem.AdminPanel
{
    public static class ViewStudent
    {
        public static void ViewStudentPanel(string className = null)
        {
            do
            {
                SMSDbContext context = new SMSDbContext();
                Class? Class;
                if(className == null)
                {
                    while(true)
                    {
                        Console.Write("Enter your class Name : ");
                        className = Console.ReadLine();

                        Class = context.Classes.Where(x => x.ClassName == className).FirstOrDefault();
                        if(Class == null)
                        {
                            Console.Clear();
                            Console.WriteLine("Class Name Not Exists in System!");
                        } 
                        else
                        {
                            break;
                        }
                    }
                } 
                else
                {
                    Class = context.Classes.Where(x => x.ClassName == className).FirstOrDefault();
                }

                Console.Clear();
                var students = context.Students.Where(x => x.ClassId == Class.ClassId).ToList();

                if(students.Count == 0)
                {
                    Console.WriteLine($"There are no students in {className}!");

                    do
                    {
                        Console.Write("\nPress 1 for go to dashboard : ");
                        int input = int.Parse(Console.ReadLine());
                        if(input == 1)
                        {
                            Console.Clear();
                            TeacherDashboard.TeacherDashboardPanel();
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input!");
                        }
                    } while (true);

                }
                
                Console.WriteLine($"\nFollowing students are present in the {className} : ");
                foreach (var student in students)
                {
                    Console.WriteLine(student.StudentName);
                }

                int teacherId = context.Users.Where(x => x.UserName == UserLogin.UserName).FirstOrDefault().UserId;
                int classId = Class.ClassId;

                var te = context.TeacherEnrollments.Where(te => te.TeacherId == teacherId && te.ClassId == classId).FirstOrDefault();

                if (te == null)
                {
                    Console.WriteLine($"\n[You can't modify {className} students!]");
                    do
                    {
                        Console.Write("\nPress 1 for go to dashboard : ");
                        int input;
                        int.TryParse(Console.ReadLine(), out input);
                        if (input == 1)
                        {
                            Console.Clear();
                            TeacherDashboard.TeacherDashboardPanel();
                        }
                        else
                        {
                            Console.WriteLine("Invalid Input!");
                        }
                    } while (true);
                }
                else
                {
                    Console.WriteLine("\nWhat you want to do?");
                    Console.WriteLine("1) Edit Student Name");
                    Console.WriteLine("2) Delete Student");
                    Console.WriteLine("3) Go to dashboard");


                    Console.Write("\nInput Your Option : ");
                    int option = int.Parse(Console.ReadLine());


                    if (option != 1 && option != 2 && option != 3)
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
                                EditStudentName.EditStudentNamePanel(classId, className);
                                break;
                            case 2:
                                DeleteStudent.DeleteStudentPanel(classId, className);
                                break;
                            case 3:
                                TeacherDashboard.TeacherDashboardPanel();
                                break;
                        }
                        break;
                    }
                }


            } while (true);

        }
    }
}
