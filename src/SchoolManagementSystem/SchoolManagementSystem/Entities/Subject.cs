using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Entities
{
    // Subject Entity
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public Class Class { get; set; }
        public int? ClassId { get; set; }
        //public List<StudentSubject> StudentSubjects { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public List<Grade> Grades { get; set; }
    }
}
