using Microsoft.Data.SqlClient;
using System.Windows;
using WPFDemoApp.Data;

namespace WPFDemoApp.Views
{
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = pwd.Password;

            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand(
                "SELECT COUNT(*) FROM Users WHERE UserName=@u AND Password=@p", con);

            cmd.Parameters.AddWithValue("@u", username);
            cmd.Parameters.AddWithValue("@p", password);

            int count = (int)cmd.ExecuteScalar();

            if (count > 0)
            {
                // Login success
                var dashboard = new DashboardView();
                dashboard.Show();

                // Close login window
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Login", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
