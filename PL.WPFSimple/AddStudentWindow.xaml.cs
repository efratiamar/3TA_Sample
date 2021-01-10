using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddStudentWindow.xaml
    /// </summary>
    public partial class AddStudentWindow : Window
    {
        IBL bl;
        BO.Student newStu = new BO.Student();


        public AddStudentWindow(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            gridOneNewStudent.DataContext = newStu;

            graduationComboBox.ItemsSource = Enum.GetValues(typeof(BO.StudentGraduate));
            statusComboBox.ItemsSource = Enum.GetValues(typeof(BO.StudentStatus));
            personalStatusComboBox.ItemsSource = Enum.GetValues(typeof(BO.PersonalStatus));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Add student?", "Verification", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (res == MessageBoxResult.No)
                return;

            //test each field

            //else
            try
            {
                
                //bl.AddStudent(newStu);
                this.Close();
            }
            catch //(BO.BadStudentIdException ex)
            {
                //MessageBox.Show(ex.Message, "Operation Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
