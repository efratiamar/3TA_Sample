using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    /// <summary>
    ///  This BO class is based on DO.Course + DO.LecturerInCourse
    ///  It is to be used inside Lecturer entity only - therefore it does not contain Person ID of the lecturer
    /// </summary>
    public class LecturerCourse
    {

        public int CourseId { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }
        public bool IsPractitioner { get; set; }
        public bool IsLecturer { get; set; }
        public int Year { get; set; }
        public Semester Semester { get; set; }
        public override string ToString() => this.ToStringProperty();

    }
}
