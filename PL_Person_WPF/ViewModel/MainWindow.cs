using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using BLAPI;

namespace ViewModel
{
    public class MainWindow : DependencyObject
    {
        IBL bl = BLFactory.GetBL("1");

        static readonly DependencyProperty StudentProperty = DependencyProperty.Register("Student", typeof(PO.Student), typeof(MainWindow));
        static readonly DependencyProperty StudentIDsProperty = DependencyProperty.Register("StudentIDs", typeof(ObservableCollection<PO.ListedPerson>), typeof(MainWindow));
        public PO.Student Student { get => (PO.Student)GetValue(StudentProperty); set => SetValue(StudentProperty, value); }
        public BO.Student StudentBO
        {
            set
            {
                value.Clone(Student);
                // update more properties in Student if needed... That is, properties that don't appear as is in studentBO...
            }
        }

        public ObservableCollection<PO.ListedPerson> StudentIDs { get => (ObservableCollection<PO.ListedPerson>)GetValue(StudentIDsProperty); set => SetValue(StudentIDsProperty, value); }

        public MainWindow() => Reset();

        internal void blGetStudent(int id)
        {
            //StudentBO
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                args.Result = bl.GetStudent((int)args.Argument);
            };
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) => StudentBO = (BO.Student)args.Result;
            StudentIDs = new ObservableCollection<PO.ListedPerson>();
            worker.RunWorkerAsync(id);
        }

        internal void Reset()
        {
            Student = new PO.Student();
            blGetStudentIDs();
        }

        public void blGetStudentIDs()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                foreach (var item in bl.GetStudentIDs())
                    worker.ReportProgress(0, item);
            };
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += (object sender, ProgressChangedEventArgs args) => StudentIDs.Add(new PO.ListedPerson() { Person = (BO.ListedPerson)args.UserState });
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) => { };
            StudentIDs = new ObservableCollection<PO.ListedPerson>();
            worker.RunWorkerAsync();
        }
    }
}
