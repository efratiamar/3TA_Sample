using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using BLAPI;

namespace ViewModel
{
    public class MainWindow : DependencyObject
    {
        IBL bl = BLFactory.GetBL("1");

        static readonly DependencyProperty StudentProperty = DependencyProperty.Register("Student", typeof(PO.Student), typeof(MainWindow));
        public PO.Student Student { get => (PO.Student)GetValue(StudentProperty); set => SetValue(StudentProperty, value); }

        static readonly DependencyProperty StudentIDsProperty = DependencyProperty.Register("StudentIDs", typeof(ObservableCollection<PO.ListedPerson>), typeof(MainWindow));
        public ObservableCollection<PO.ListedPerson> StudentIDs { get => (ObservableCollection<PO.ListedPerson>)GetValue(StudentIDsProperty); set => SetValue(StudentIDsProperty, value); }

        public BO.Student StudentBO
        {
            set
            {
                if (value == null)
                    Student = new PO.Student();
                else
                {
                    value.DeepCopyTo(Student);
                    //Student.ID = value.ID;
                    ////...
                    //Student.ListOfCourses.Clear();
                    //foreach (var fromCourse in value.ListOfCourses)
                    //{
                    //    PO.StudentCourse toCourse = new PO.StudentCourse();
                    //    toCourse.Grade = fromCourse.Grade;
                    //    toCourse. Number = fromCourse.Number;
                    //    // ...
                    //    Student.ListOfCourses.Add(toCourse);
                    //}
                }
                // update more properties in Student if needed... That is, properties that don't appear as is in studentBO...
            }
        }

        public MainWindow() => Reset();

        BackgroundWorker getStudentWorker;
        internal void blGetStudent(int id)
        {
            if (getStudentWorker != null)
                getStudentWorker.CancelAsync();
            getStudentWorker = new BackgroundWorker();
            getStudentWorker.WorkerSupportsCancellation = true;
            getStudentWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) =>
            {
                if (!((BackgroundWorker)sender).CancellationPending)
                    StudentBO = (BO.Student)args.Result;
            };
            getStudentWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                object student = bl.GetStudent((int)args.Argument);
                args.Result = worker.CancellationPending ? null : student;
            };
            getStudentWorker.RunWorkerAsync(id);
        }

        internal void Reset()
        {
            if (getStudentWorker != null)
            {
                getStudentWorker.CancelAsync();
                getStudentWorker = null;
            }
            if (getStudentIDsWorker != null)
            {
                getStudentIDsWorker.CancelAsync();
                getStudentIDsWorker = null;
            }
            Student = new PO.Student();
            blGetStudentIDs();
        }

        BackgroundWorker getStudentIDsWorker;
        public void blGetStudentIDs()
        {
            getStudentIDsWorker = new BackgroundWorker();
            getStudentIDsWorker.WorkerSupportsCancellation = true;
            getStudentIDsWorker.WorkerReportsProgress = true;
            getStudentIDsWorker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs args) => getStudentIDsWorker = null;
            getStudentIDsWorker.ProgressChanged += (object sender, ProgressChangedEventArgs args) =>
            {
                if (!((BackgroundWorker)sender).CancellationPending)
                    StudentIDs.Add(new PO.ListedPerson() { Person = (BO.ListedPerson)args.UserState });
            };
            getStudentIDsWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                BackgroundWorker worker = (BackgroundWorker)sender;
                foreach (var item in bl.GetStudentIDNameList())
                {
                    if (worker.CancellationPending) break;
                    worker.ReportProgress(0, item);
                }
            };
            StudentIDs = new ObservableCollection<PO.ListedPerson>();
            getStudentIDsWorker.RunWorkerAsync();
        }
    }
}
