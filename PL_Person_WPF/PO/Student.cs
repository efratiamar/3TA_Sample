using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PO
{
    public class Student : Person
    {
        static readonly DependencyProperty StartYearProperty = DependencyProperty.Register("StartYear", typeof(int), typeof(Student));
        static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(BO.StudentStatus), typeof(Student));
        static readonly DependencyProperty GraduationProperty = DependencyProperty.Register("Graduation", typeof(BO.StudentGraduate), typeof(Student));
        //static readonly DependencyProperty ListOfCoursesProperty = DependencyProperty.Register("ListOfCourses", typeof(ObservableCollection<StudentCourse>), typeof(Student));

        public int StartYear { get => (int)GetValue(StartYearProperty); set => SetValue(StartYearProperty, value); }
        public BO.StudentStatus Status { get => (BO.StudentStatus)GetValue(StatusProperty); set => SetValue(StatusProperty, value); }
        public BO.StudentGraduate Graduation { get => (BO.StudentGraduate)GetValue(GraduationProperty); set => SetValue(GraduationProperty, value); }
        public ObservableCollection<StudentCourse> ListOfCourses { get; } = new ObservableCollection<StudentCourse>();
        // { get => (ObservableCollection<StudentCourse>)GetValue(ListOfCoursesProperty); set => SetValue(ListOfCoursesProperty, value); }
        //public Student()
        //{
        //    ListOfCourses = new ObservableCollection<StudentCourse>();
        //}
    }
}
