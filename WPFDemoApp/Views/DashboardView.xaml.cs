using System.Windows;
using WPFDemoApp.Data;
using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using WPFDemoApp.Models;

namespace WPFDemoApp.Views
{
    public partial class DashboardView : Window
    {
        private ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();

        public DashboardView()
        {
            InitializeComponent();
            dgEmployees.ItemsSource = Employees;
            LoadEmployees();
        }

        private void Employee_Click(object sender, RoutedEventArgs e)
        {
            var empView = new EmployeeView();
            empView.Show();
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            // Close dashboard and return to login
            var login = new LoginView();
            login.Show();
            this.Close();
        }

        private void LoadEmployees()
        {
            Employees.Clear();
            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand("SELECT * FROM Employees", con);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Employees.Add(new Employee
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    Department = reader["Department"].ToString()
                });
            }
        }
    }
}
