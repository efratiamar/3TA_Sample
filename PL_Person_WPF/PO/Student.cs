using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PO
{
    class Student : Person
    {
        public int StartYear { get; set; }
        public BO.StudentStatus Status { get; set; }
        public BO.StudentGraduate Graduation { get; set; }
        public IEnumerable<StudentCourse> listOfCourses { get; set; }
    }
}
