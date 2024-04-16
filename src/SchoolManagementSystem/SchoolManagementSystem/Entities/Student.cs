using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Entities
{
    // Student entity
    public class Student
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public int ClassId { get; set; }
        public Class Class { get; set; }
        //public List<StudentSubject> StudentSubjects { get; set; }
        public List<Grade> Grades { get; set; }
    }
}
