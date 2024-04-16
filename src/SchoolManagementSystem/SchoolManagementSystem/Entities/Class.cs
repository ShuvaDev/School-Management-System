using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Entities
{
    // Class Entity
    public class Class
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public List<Student> Students { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<TeacherEnrollment> TeacherEnrollments { get; set; }
    }
}
