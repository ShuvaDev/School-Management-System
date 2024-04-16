using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.AdminPanel
{
    public class AdminDashboard
    {
        public static void AdminDashboardPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine($"Welcome {UserLogin.UserName}, please select an option : \n");
                    Console.WriteLine("1) Create class");
                    Console.WriteLine("2) Create Subject");
                    Console.WriteLine("3) Create Teacher");
                    Console.WriteLine("4) View Classes");
                    Console.WriteLine("5) View Subjects");
                    Console.WriteLine("6) View Teachers");
                    Console.WriteLine("7) Logout");

                    Console.Write("\nInput Your Option : ");
                    int option;
                    int.TryParse(Console.ReadLine(), out option);

                    if (option != 1 && option != 2 && option != 3 && option != 4 && option != 5 && option != 6 && option != 7)
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
                                CreateClass.CreateClassPanel();
                                break;
                            case 2:
                                CreateSubject.CreateSubjectPanel();
                                break;
                            case 3:
                                CreateTeacher.CreateTeacherPanel();
                                break;
                            case 4:
                                ViewClass.ViewClassPanel();
                                break;
                            case 5:
                                ViewSubject.ViewSubjectPanel();
                                break;
                            case 6:
                                ViewTeacher.ViewTeacherPanel();
                                break;
                            case 7:
                                UserLogin.UserLoginPanel();
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
