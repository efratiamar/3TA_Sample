using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Course
    {
        public int ID { get; set; } // automatic number
        public int Number { get; set; }
        public string Name { get; set; }
        public int LectureHours { get; set; }
        public int PracticeHours { get; set; }
        public float CreditPoint { get; set; } // 0.5 is the smallest fraction
        public int Year { get; set; }
        public Semester Semester { get; set; }

    }
}
