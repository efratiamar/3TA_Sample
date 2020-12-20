using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Student
    {
        public int ID { get; set; } // person ID
        public int StartYear { get; set; }
        public StudentStatus Status { get; set; }
        public StudentGraduate Graduation { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
