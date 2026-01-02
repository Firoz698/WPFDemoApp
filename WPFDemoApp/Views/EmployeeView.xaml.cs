using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using WPFDemoApp.Models;

namespace WPFDemoApp.Views
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public class EmployeeViewModel
    {
        public ObservableCollection<Employee> Employees { get; set; } = new();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string SearchText { get; set; }

        public ICommand SaveCommand { get; }

        public EmployeeViewModel()
        {
            SaveCommand = new RelayCommand(Save);
            LoadEmployees();
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

        private void Save(object obj)
        {
            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand(
                "INSERT INTO Employees VALUES (@n,@e,@d)", con);

            cmd.Parameters.AddWithValue("@n", Name);
            cmd.Parameters.AddWithValue("@e", Email);
            cmd.Parameters.AddWithValue("@d", Department);

            cmd.ExecuteNonQuery();
            LoadEmployees();
        }
    }
}
