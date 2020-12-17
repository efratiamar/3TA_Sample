using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    ///  This BO class is based on DO.Course + DO.StudentInCourse
    ///  It is to be used inside Student entity only - therefore it does not contain Person ID of the student
    /// </summary>
    public class StudentCourse
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int? Grade { get; set; }
        public int Year { get; set; }
        public Semester Semester { get; set; }
        public override string ToString() => this.ToStringProperty();
    }
}
