using Microsoft.Data.SqlClient;
using System.Collections.ObjectModel;
using System.Windows;
using WPFDemoApp.Data;
using WPFDemoApp.Models;

namespace WPFDemoApp.Views
{
    public partial class EmployeeView : Window
    {
        private ObservableCollection<Employee> Employees = new ObservableCollection<Employee>();

        public EmployeeView()
        {
            InitializeComponent();
            dgEmployees.ItemsSource = Employees;
            LoadEmployees();
        }

        private void LoadEmployees(string search = "")
        {
            Employees.Clear();

            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand(
                "SELECT * FROM Employees WHERE Name LIKE @s OR Email LIKE @s OR Department LIKE @s", con);
            cmd.Parameters.AddWithValue("@s", $"%{search}%");

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

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand(
                "INSERT INTO Employees (Name, Email, Department) VALUES (@n,@e,@d)", con);
            cmd.Parameters.AddWithValue("@n", txtName.Text);
            cmd.Parameters.AddWithValue("@e", txtEmail.Text);
            cmd.Parameters.AddWithValue("@d", txtDepartment.Text);

            cmd.ExecuteNonQuery();
            LoadEmployees();

            MessageBox.Show("Employee saved!");
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgEmployees.SelectedItem is Employee emp)
            {
                using var con = new AppDbContext().GetConnection();
                con.Open();

                var cmd = new SqlCommand("DELETE FROM Employees WHERE Id=@id", con);
                cmd.Parameters.AddWithValue("@id", emp.Id);

                cmd.ExecuteNonQuery();
                LoadEmployees();

                MessageBox.Show("Employee deleted!");
            }
            else
            {
                MessageBox.Show("Please select an employee to delete.");
            }
        }

        private void TxtSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            LoadEmployees(txtSearch.Text);
        }
    }
}
