using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SchoolManagementSystem.Entities;
using SchoolManagementSystem.TeacherPanle;

namespace SchoolManagementSystem.TeacherPanel
{
    public static class ViewGrade
    {
        private static SMSDbContext context;
        public static void ViewGradePanel()
        {
            do
            {
                Console.WriteLine("Please provide following information to view grades : ");
                Console.Write("Class Name : ");
                string className = Console.ReadLine();

                if (String.IsNullOrWhiteSpace(className))
                {
                    Console.Clear();
                    Console.WriteLine("Class Name Can't be Empty!");
                } 
                else
                {
                    context = new();
                    Class Class = context.Classes.Where(x => x.ClassName == className).FirstOrDefault();
                    if(Class == null)
                    {
                        Console.Clear();
                        Console.WriteLine($"Class Name '{className}' Not Exists in System!");
                    }
                    else
                    {
                        Console.Clear();
                        GenerateGradeList(Class.ClassId, className);
                    }
                }

            } while (true);
        }
        public static void GenerateGradeList(int classId, string className)
        {
            Console.Clear();
            Console.WriteLine($"Showing grades for - {className} : ");
            var students = context.Students.Where(x => x.ClassId == classId).Include(x => x.Grades).ToList();

            if(students.Count == 0)
            {
                Console.WriteLine($"\nThere are no student in {className}!\n");
            }
            else
            {


                // full grade list
                Console.WriteLine("\n\nFull Grade List : ");
                
                List<Subject> subjects = context.Subjects.Where(s => s.ClassId == classId).ToList();
                if(subjects.Count == 0) Console.WriteLine($"There are no subjects in {className}!");
                else
                {
                    string heading1 = "|   ";
                    foreach (var subject in subjects)
                    {
                        heading1 = string.Concat(heading1, subject.SubjectName.PadRight(12));
                    }

                    Console.Write(" ");
                    for (int i = 1; i <= heading1.Length * 3 + 10; i++) Console.Write("_");
                    Console.WriteLine();

                    Console.Write("|          "); ;
                    string term_heading = String.Concat("|   ", "1st Term".PadRight(heading1.Length - 4), "|   ", "Mid Term".PadRight(heading1.Length - 4), "|   ", "Final Term".PadRight(heading1.Length - 4), "|");
                    Console.WriteLine(term_heading);

                    Console.Write("|");
                    for (int i = 1; i <= heading1.Length * 3 + 10; i++) Console.Write("-");
                    Console.WriteLine("|");

                    Console.Write("|          "); ;
                    Console.WriteLine(heading1 + heading1 + heading1 + "|");


                    Console.Write("|");
                    for (int i = 1; i <= heading1.Length * 3 + 10; i++) Console.Write("_");
                    Console.WriteLine("|");



                    foreach (var student in students)
                    {
                        string row = "|";
                        row = row + student.StudentName.PadRight(10);

                        if (student.Grades == null)
                        {
                            string h1 = "|   ";
                            foreach (var subject in subjects)
                            {
                                h1 = string.Concat(h1, "-".PadRight(12));
                            }
                            row += h1 + h1 + h1 + "|";
                        }
                        else
                        {
                            string sfirstt_result = "|   ";
                            string smidt_result = "|   ";
                            string sfinalt_result = "|   ";

                            foreach (var subject in subjects)
                            {
                                double? r1 = student.Grades.Where(g => g.SubjectId == subject.SubjectId).Select(g => g.first_term).FirstOrDefault();
                                double? r2 = student.Grades.Where(g => g.SubjectId == subject.SubjectId).Select(g => g.mid_term).FirstOrDefault();
                                double? r3 = student.Grades.Where(g => g.SubjectId == subject.SubjectId).Select(g => g.final_term).FirstOrDefault();


                                if (r1.HasValue) sfirstt_result += r1.Value.ToString().PadRight(12);
                                else sfirstt_result += "-".PadRight(12);
                                if (r2.HasValue) smidt_result += r2.Value.ToString().PadRight(12);
                                else smidt_result += "-".PadRight(12);
                                if (r3.HasValue) sfinalt_result += r3.Value.ToString().PadRight(12);
                                else sfinalt_result += "-".PadRight(12);

                            }
                            row = row + sfirstt_result + smidt_result + sfinalt_result + "|";
                        }

                        Console.WriteLine(row);
                    }

                    Console.Write("|");
                    for (int i = 1; i <= heading1.Length * 3 + 10; i++) Console.Write("_");
                    Console.WriteLine("|");
                }

                // Average result based on current input
                Console.WriteLine("\n\nAverage Result Based on Current Input : ");
                string heading2 = String.Concat("\n    Name".PadRight(25), "1st".PadRight(10), "Mid".PadRight(10), "Final".PadRight(10));
                Console.WriteLine(heading2);
                for (int i = 1; i <= heading2.Length; i++) Console.Write("_");
                Console.WriteLine();

                foreach (var student in students)
                {
                    string row;
                    if (student.Grades == null)
                    {
                        row = String.Concat($"    {student.StudentName}".PadRight(23), "-".PadRight(10), "-".PadRight(10), "-".PadRight(10));
                    }
                    else
                    {


                        double first_term_avg, mid_term_avg, final_term_avg;
                        first_term_avg = student.Grades.Select(g => g.first_term).Sum().Value / student.Grades.Where(g => g.first_term != null).Select(g => g.first_term).Count();
                        mid_term_avg = student.Grades.Select(g => g.mid_term).Sum().Value / student.Grades.Where(g => g.mid_term != null).Select(g => g.mid_term).Count();
                        final_term_avg = student.Grades.Select(g => g.final_term).Sum().Value / student.Grades.Where(g => g.final_term != null).Select(g => g.final_term).Count();

                        string sfirst_term_avg = double.IsNaN(first_term_avg) ? "-" : first_term_avg.ToString();
                        string smid_term_avg = double.IsNaN(mid_term_avg) ? "-" : mid_term_avg.ToString();
                        string sfinal_term_avg = double.IsNaN(final_term_avg) ? "-" : final_term_avg.ToString();

                        row = String.Concat($"    {student.StudentName}".PadRight(23), $"{sfirst_term_avg}".PadRight(10), $"{smid_term_avg}".PadRight(10), $"{sfinal_term_avg}".PadRight(10));
                    }
                    
                    Console.WriteLine(row);
                }
                for (int i = 1; i <= heading2.Length; i++) Console.Write("_");
                Console.WriteLine();
            }

            do
            {
                Console.Write("\nPress 1 for go to Dashboard : ");
                int option;
                int.TryParse(Console.ReadLine(), out option);

                if(option == 1)
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
    }
}
