using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchoolManagementSystem.AdminPanel;
using SchoolManagementSystem.TeacherPanel;

namespace SchoolManagementSystem.TeacherPanle
{
    public static class TeacherDashboard
    {
        public static void TeacherDashboardPanel()
        {
            do
            {
                Console.WriteLine($"Welcome {UserLogin.UserName}, please select an option : \n");
                Console.WriteLine("1) View grades");
                Console.WriteLine("2) Insert grades");
                Console.WriteLine("3) Add students");
                Console.WriteLine("4) View students");
                Console.WriteLine("5) Logout");

                Console.Write("\nInput Your Option : ");
                int option;
                int.TryParse(Console.ReadLine(), out option);

                if (option != 1 && option != 2 && option != 3 && option != 4 && option != 5)
                {
                    Console.Clear();
                    Console.WriteLine("Invalid Input!\n");
                }
                else
                {
                    switch (option)
                    {
                        case 1:
                            Console.Clear();
                            ViewGrade.ViewGradePanel();
                            Console.Clear();
                            break;
                        case 2:
                            Console.Clear();
                            InsertGrade.InsertGradePanel();
                            break;
                        case 3:
                            Console.Clear();
                            AddStudent.AddStudentPanel();
                            break;
                        case 4:
                            Console.Clear();
                            ViewStudent.ViewStudentPanel();
                            break;
                        case 5:
                            Console.Clear();
                            UserLogin.UserLoginPanel();
                            break;
                    }
                    break;
                }
            } while (true);
        }
    }
}
