using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.AdminPanel
{
    public static class ViewSubject
    {
        public static void ViewSubjectPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Following subjects are present in the system : ");
                    SMSDbContext context = new SMSDbContext();
                    var Subjects = context.Subjects.ToList();
                    if(Subjects.Count == 0) Console.WriteLine("There is no subject!");
                    else
                    {
                        foreach (var Subject in Subjects)
                        {
                            Console.WriteLine(Subject.SubjectName);
                        }
                    }

                    Console.WriteLine("\nWhat you wnat to do?");
                    Console.WriteLine("1) Edit Subject");
                    Console.WriteLine("2) Delete Subject");
                    Console.WriteLine("3) Assign Teacher");
                    Console.WriteLine("4) Go to dashboard");


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
                                EditSubject.EditSubjectPanel();
                                break;
                            case 2:
                                DeleteSubject.DeleteSubjectPanel();
                                break;
                            case 3:
                                AssignTeacher.AssignTeacherPanel();
                                break;
                            case 4:
                                AdminDashboard.AdminDashboardPanel();
                                break;
                        }
                        break;
                    }
                } while (true);
            }
            else
            {
                Console.WriteLine("Access Denied!");
            }

        }
    }
}
