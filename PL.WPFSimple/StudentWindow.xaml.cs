using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using BLAPI;

namespace PL.SimpleWPF
{
    /// <summary>
    /// Interaction logic for Student.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {

        IBL bl;
        BO.Student curStu;
        public StudentWindow(IBL _bl)
        {
            InitializeComponent();

            bl = _bl;

            graduationComboBox.ItemsSource = Enum.GetValues(typeof(BO.StudentGraduate));
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.StudentStatus));
            personalStatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.PersonalStatus));

            cbStudentID.DisplayMemberPath = "Name";//show only specific Property of object
            cbStudentID.SelectedValuePath = "ID";//selection return only specific Property of object
            cbStudentID.SelectedIndex = 0; //index of the object to be selected
            RefreshAllStudentComboBox();

            studentCourseDataGrid.IsReadOnly = true;
            courseDataGrid.IsReadOnly = true;
            
        }

        void RefreshAllStudentComboBox()
        {
            cbStudentID.DataContext = bl.GetAllStudents().ToList(); //ObserListOfStudents;
        }

        void RefreshAllRegisteredCoursesGrid()
        {
            studentCourseDataGrid.DataContext = curStu.ListOfCourses.ToList();
        }

        void RefreshAllNotRegisteredCoursesGrid()
        {
            List<BO.Course> listOfUnRegisteredCourses = bl.GetAllCourses().Where(c1 => curStu.ListOfCourses.All(c2 => c2.ID != c1.ID)).ToList();
            courseDataGrid.DataContext = listOfUnRegisteredCourses;
        }

        private void cbStudentID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            curStu = (cbStudentID.SelectedItem as BO.Student);
            gridOneStudent.DataContext = curStu;

            if (curStu != null)
            {
                //list of courses of selected student
                RefreshAllRegisteredCoursesGrid();
                //list of all courses (that selected student is not registered to it)
                RefreshAllNotRegisteredCoursesGrid();                
            }
        }

        private void btUpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (curStu != null)
                    bl.UpdateStudentPersonalDetails(curStu);
            }
            catch (BO.BadStudentIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btDeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Delete selected student?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            try
            {
                if (curStu != null)
                {
                    bl.DeleteStudent(curStu.ID);
                    BO.Student stuToDel = curStu;
                    
                    RefreshAllRegisteredCoursesGrid();
                    RefreshAllNotRegisteredCoursesGrid();
                    RefreshAllStudentComboBox();
                }
            }
            catch(BO.BadStudentIdCourseIDException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (BO.BadStudentIdException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btUpdateGradeInCourse_Click(object sender, RoutedEventArgs e)
        {
            BO.StudentCourse scBO = ((sender as Button).DataContext as BO.StudentCourse);
            GradeWindow win = new GradeWindow(scBO);
            win.Closing += WinUpdateGrade_Closing;
            win.ShowDialog();
        }

        private void WinUpdateGrade_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BO.StudentCourse scBO = (sender as GradeWindow).curScBO; 
            
            MessageBoxResult res = MessageBox.Show("Update grade for selected student?", "Verification", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
            {
                (sender as GradeWindow).cbGrade.Text = (sender as GradeWindow).gradeBeforeUpdate.ToString();
            }
            else if (res == MessageBoxResult.Cancel)
            {
                (sender as GradeWindow).cbGrade.Text = (sender as GradeWindow).gradeBeforeUpdate.ToString();
                e.Cancel = true; //window stayed open. cancel closing event.
            }
            else
            {
                try
                {
                    bl.UpdateStudentGradeInCourse(curStu.ID, scBO.ID, (float)scBO.Grade);
                }
                catch (BO.BadStudentIdCourseIDException ex)
                {
                    MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void btUnRegisterCourse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BO.StudentCourse scBO = ((sender as Button).DataContext as BO.StudentCourse);
                bl.DeleteStudentInCourse(curStu.ID, scBO.ID);
                RefreshAllRegisteredCoursesGrid();
                RefreshAllNotRegisteredCoursesGrid();
            }
            catch(BO.BadStudentIdCourseIDException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btRegisterCourse_Click(object sender, RoutedEventArgs e)
        {
            if (curStu == null)
            {
                MessageBox.Show("You must select a student first", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                BO.Course cBO = ((sender as Button).DataContext as BO.Course);
                bl.AddStudentInCourse(curStu.ID, cBO.ID);

                RefreshAllRegisteredCoursesGrid();
                RefreshAllNotRegisteredCoursesGrid();
            }
            catch (BO.BadStudentIdCourseIDException ex)
            {
                MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void btAddStudent_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This method is under construction!", "TBD", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
