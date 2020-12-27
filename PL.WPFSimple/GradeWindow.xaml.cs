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

namespace PL.SimpleWPF
{
    /// <summary>
    /// Interaction logic for GradeWindow.xaml
    /// </summary>
    public partial class GradeWindow : Window
    {
        public BO.StudentCourse curScBO;
        public float gradeBeforeUpdate;
        public GradeWindow(BO.StudentCourse scBO)
        {
            InitializeComponent();
            curScBO = scBO;
            gradeBeforeUpdate = (float)scBO.Grade;

            for (int i = 0; i <= 100; i++)
            {
                cbGrade.Items.Add(i);
            }

            DataContext = curScBO;
        }


        private void btUpdateGrade_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
