using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Student : Person
    {
        public int StartYear { get; set; }
        public StudentStatus Status { get; set; }
        public StudentGraduate Graduation { get; set; }
        public IEnumerable<StudentCourse> ListOfCourses {get;set;}
        public override string ToString() => this.ToStringProperty();
    }
}
