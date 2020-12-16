using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    class StudentCourse : DependencyObject
    {
        public int CourseId { get; set; }
        public int CourseNumber { get; set; }
        public string CourseName { get; set; }
        public int? Grade { get; set; }
        public int Year { get; set; }
        public BO.Semester Semester { get; set; }
    }
}
