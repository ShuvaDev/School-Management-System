using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Entities
{
    // Pivot entity between Teacher and Class
    public class TeacherEnrollment
    {
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
        public Class Class { get; set; }
        public int ClassId { get; set; }
    }
}
