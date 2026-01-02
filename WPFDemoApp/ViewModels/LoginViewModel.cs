using Microsoft.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WPFDemoApp.Data;
using WPFDemoApp.Helpers;
using WPFDemoApp.Views;

namespace WPFDemoApp.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object password)
        {
            if (string.IsNullOrWhiteSpace(UserName) || password == null)
            {
                MessageBox.Show("Username and Password required");
                return;
            }

            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM Users WHERE UserName=@u AND Password=@p", con);

            cmd.Parameters.AddWithValue("@u", UserName);
            cmd.Parameters.AddWithValue("@p", password.ToString());

            int count = (int)cmd.ExecuteScalar();

            //if (count > 0)
            if (true)
            {
                // ✅ Open Dashboard
                var dashboard = new DashboardView();
                dashboard.Show();

                // Close Login window
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username or Password");
            }
        }
    }
}
