using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Entities
{
    // Teacher Entity
    public class Teacher
    {
        public int TeacherId { get; set; }
        public List<Subject> Subjects { get; set; }
        public List<TeacherEnrollment> TeacherEnrollments { get; set; }

    }
}
