using System.Windows;

namespace WPFDemoApp.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : Window
    {
        public DashboardView()
        {
            InitializeComponent();
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            var empView = new EmployeeView();
            empView.Show();
        }
    }
}
