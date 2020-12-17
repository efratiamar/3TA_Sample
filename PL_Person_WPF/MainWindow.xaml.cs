using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


using BLAPI;

namespace PL_Person_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    partial class MainWindow : Window
    {
        public ViewModel.MainWindow viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel.MainWindow();
            //viewModel.Reset();
            DataContext = viewModel;
        }

        private void cbStudentID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int id;

            if (cbStudentID.SelectedIndex < 0)
                return; 

            try
            {
                id = (int)cbStudentID.SelectedValue;
                viewModel.blGetStudent(id);
                //gridStudent.DataContext = studentBO;
                //courses = new ObservableCollection<BO.StudentCourse>(studentBO.listOfCourses);
                //studentCourseDataGrid.DataContext = courses;
                //studentCourseDataGrid.DataContext = studentBO.listOfCourses;

                //cbStudentID.Text = ((PO.ListedPerson)cbStudentID.SelectedItem).Show;
                //cbStudentID.IsDropDownOpen = false;
                //gridStudent.Visibility = Visibility.Visible;
                //btReset.IsEnabled = true;
                //MessageBox.Show(studentBO.ToString(), "Student");
            }
            catch (BO.BadStudentIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbStudentID_DropDownOpened(object sender, EventArgs e) => cbStudentID_Handle();
        private void cbStudentID_KeyUp(object sender, KeyEventArgs e) => cbStudentID_Handle();
        private void cbStudentID_Handle()
        {
            if (!cbStudentID.IsDropDownOpen) return;
            var ctb = cbStudentID.Template.FindName("PART_EditableTextBox", cbStudentID) as TextBox;
            if (ctb == null) return;
            var caretPos = ctb.CaretIndex;

            CollectionView itemsViewOriginal = (CollectionView)CollectionViewSource.GetDefaultView(cbStudentID.Items);
            itemsViewOriginal.Filter = ((o) =>
            {
                if (String.IsNullOrEmpty(cbStudentID.Text))
                {
                    return true;
                }
                else
                {
                    if (((PO.ListedPerson)o).Show.Contains(cbStudentID.Text))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            });

            itemsViewOriginal.Refresh();
            ctb.CaretIndex = caretPos;
        }

        private void btReset_Click(object sender, RoutedEventArgs e)
        {
            //cbStudentID.Text = "";
            cbStudentID.SelectedIndex = -1;
            //btReset.IsEnabled = false;
            //gridStudent.Visibility = Visibility.Hidden;
            viewModel.Reset();
        }
    }
}
