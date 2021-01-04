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

namespace PL.WPF.Person
{
    /// <summary>
    /// Interaction logic for Trials.xaml
    /// </summary>
    public partial class Trials : Window
    {
        ViewModel.MainWindow viewModel;

        public Trials(ViewModel.MainWindow viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listedPersonDataGrid.DataContext = viewModel.StudentIDs;
            System.Windows.Data.CollectionViewSource listedPersonViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("listedPersonViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // listedPersonViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource studentViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("studentViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // studentViewSource.Source = [generic data source]
        }
    }
}
