using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class StudentCourse : DependencyObject
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public int? Grade { get; set; }
        public int Year { get; set; }
        public BO.Semester Semester { get; set; }
    }
}
