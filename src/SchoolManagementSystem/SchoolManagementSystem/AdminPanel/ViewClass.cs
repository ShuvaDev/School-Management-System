using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.AdminPanel
{
    public static class ViewClass
    {
        public static void ViewClassPanel()
        {
            if (UserLogin.UserType)
            {
                do
                {
                    Console.WriteLine("Following classes are present in the system : ");
                    SMSDbContext context = new SMSDbContext();
                    var Classes = context.Classes.ToList();
                    if(Classes.Count == 0) Console.WriteLine("There is no class!");
                    else
                    {
                        foreach (var Class in Classes)
                        {
                            Console.WriteLine(Class.ClassName);
                        }
                    }

                    Console.WriteLine("\nWhat you wnat to do?");
                    Console.WriteLine("1) Edit class");
                    Console.WriteLine("2) Delete class");
                    Console.WriteLine("3) Assign subject");
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
                                EditClass.EditClassPanel();
                                break;
                            case 2:
                                DeleteClass.DeleteClassPanel();
                                break;
                            case 3:
                                AssignSubject.AssignSubjectPanel();
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
