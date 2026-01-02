using Microsoft.Data.SqlClient;
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
using WPFDemoApp.Data;
using WPFDemoApp.Helpers;
using System.Data.SqlClient;

namespace WPFDemoApp.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        private void Login(object password)
        {
            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM Users WHERE UserName=@u AND Password=@p", con);

            cmd.Parameters.AddWithValue("@u", UserName);
            cmd.Parameters.AddWithValue("@p", password?.ToString());

            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                new DashboardView().Show();
                Application.Current.MainWindow.Close();
            }
            else
            {
                MessageBox.Show("Invalid Login");
            }
        }
    }
}
