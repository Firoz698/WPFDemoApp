using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WPFDemoApp.Data;
using WPFDemoApp.Helpers;
using WPFDemoApp.Models;
using Microsoft.Data.SqlClient;


namespace WPFDemoApp.ViewModels
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Employee> Employees { get; set; } = new();

        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        private string _department;
        public string Department
        {
            get => _department;
            set { _department = value; OnPropertyChanged(); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                LoadEmployees();
            }
        }

        public ICommand SaveCommand { get; }
        public ICommand DeleteCommand { get; }

        public EmployeeViewModel()
        {
            SaveCommand = new RelayCommand(_ => Save());
            DeleteCommand = new RelayCommand(_ => Delete());
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            Employees.Clear();

            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand(
                "SELECT * FROM Employees WHERE Name LIKE @s OR Email LIKE @s", con);

            cmd.Parameters.AddWithValue("@s", $"%{SearchText}%");

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

        private void Save()
        {
            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand(
                "INSERT INTO Employees (Name, Email, Department) VALUES (@n,@e,@d)", con);

            cmd.Parameters.AddWithValue("@n", Name);
            cmd.Parameters.AddWithValue("@e", Email);
            cmd.Parameters.AddWithValue("@d", Department);

            cmd.ExecuteNonQuery();
            LoadEmployees();
        }

        private void Delete()
        {
            if (Employees.Count == 0) return;

            var emp = Employees[^1];

            using var con = new AppDbContext().GetConnection();
            con.Open();

            var cmd = new SqlCommand("DELETE FROM Employees WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@id", emp.Id);

            cmd.ExecuteNonQuery();
            LoadEmployees();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
