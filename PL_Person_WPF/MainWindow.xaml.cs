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
using System.Windows.Navigation;
using System.Windows.Shapes;

using BLAPI;

namespace PL_Person_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IBL bl;
        public MainWindow()
        {
            InitializeComponent();

            bl = BLFactory.GetBL("test");
        }

        private void btGetStudentByID_Click(object sender, RoutedEventArgs e)
        {
            int id;
            try
            {
                id = int.Parse(txtStudentID.Text);

                bl.GetStudent(id);
            }
            catch (FormatException)
            {
                MessageBox.Show($"Unable to parse {txtStudentID.Text}");
            }
            catch (BO.BadStudentIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGetAllPersonsByPerSta_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
