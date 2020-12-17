using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ViewModel
{
    public class MainWindow : DependencyObject
    {
        BLAPI.IBL bl;
        static readonly DependencyProperty StudentProperty = DependencyProperty.Register("Student", typeof(PO.Student), typeof(MainWindow));
        static readonly DependencyProperty StudentIDsProperty = DependencyProperty.Register("StudentIDs", typeof(IEnumerable<PO.ListedPerson>), typeof(MainWindow));
        public PO.Student Student { get => (PO.Student)GetValue(StudentProperty); set => SetValue(StudentProperty, value); }
        public BO.Student StudentBO { set
            {
                value.Clone(Student);
                // update more properties in Student if needed... That is, properties that don't appear as is in studentBO...
            }
        }

        public IEnumerable<PO.ListedPerson> StudentIDs { get => (IEnumerable<PO.ListedPerson>)GetValue(StudentIDsProperty); set => SetValue(StudentIDsProperty, value); }

        public MainWindow(BLAPI.IBL bl)
        {
            this.bl = bl;
            Reset();
        }
        internal void Reset()
        {
            Student = new PO.Student();
            StudentIDs = from p in bl.GetStudentIDs()
                         select new PO.ListedPerson() { Person = p };
        }
    }
}
