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

namespace Partea1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource titluViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("titluViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // titluViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource doctorViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("doctorViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // doctorViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource pontajViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("pontajViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // pontajViewSource.Source = [generic data source]
            System.Windows.Data.CollectionViewSource concediuViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("concediuViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // concediuViewSource.Source = [generic data source]
        }
    }
}
