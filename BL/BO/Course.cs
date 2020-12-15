using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Course
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public int LectureHours { get; set; }
        public int PracticeHours { get; set; }
        public float CreditPoint { get; set; } // 0.5 is the smallest fraction
        public int Year { get; set; }
        public Semester Semester { get; set; }
        public IEnumerable<CourseLecturer> Lecturers { get; set; }
    }
}
