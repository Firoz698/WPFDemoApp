using System.Windows;
using System.Windows.Input;
using WPFDemoApp.Helpers;
using WPFDemoApp.Views;

namespace WPFDemoApp.ViewModels
{
    public class DashboardViewModel
    {
        public ICommand OpenEmployeeCommand { get; }
        public ICommand LogoutCommand { get; }

        public DashboardViewModel()
        {
            OpenEmployeeCommand = new RelayCommand(_ => OpenEmployee());
            LogoutCommand = new RelayCommand(_ => Logout());
        }

        private void OpenEmployee()
        {
            var empView = new EmployeeView();
            empView.Show();
        }

        private void Logout()
        {
            var loginView = new LoginView();
            loginView.Show();

            Application.Current.MainWindow?.Close();
        }
    }
}
