using Kolokwium.DAL.EF;
using Kolokwium.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
using Microsoft.EntityFrameworkCore;

namespace Kolokwium.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ApplicationDbContext _dbContext;
        private bool _isDepartments = true;
        public MainWindow(ApplicationDbContext dbContext)
        {

            _dbContext = dbContext;
            InitializeComponent();
            DataGridDepartments.DataContext = _dbContext.departments;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetGrid(DataGridDepartments, _dbContext.departments.Include(dep => dep.Employees));
        }
        internal static void SetGrid<T>(DataGrid dataGrid, IEnumerable<T> list) where T : new()
        {
            dataGrid.Columns.Clear();
            var type = typeof(T);
            foreach (var prop in type.GetProperties())
                if (prop.GetCustomAttribute<HideAttribute>() == null)
                    dataGrid.Columns.Add(new DataGridTextColumn()
                    {
                        Header = prop.Name,
                        Binding = new Binding(prop.Name)
                    });
            dataGrid.AutoGenerateColumns = false;
            dataGrid.ItemsSource = list;
            dataGrid.Items.Refresh();
        }//asdfasdf
        private void AddEditDepartmentOrEmployeeClick(object sender, RoutedEventArgs e)
        {
            if(AddEditDepartmentButton.Content.ToString() == "Add Or Edit Department")
            {
                if (DataGridDepartments.SelectedItem != null &&
                DataGridDepartments.SelectedItem is Department d)
                {
                    AddEditDepartmentWindow addEditDepartmentWindow = new AddEditDepartmentWindow(_dbContext, d);
                    if (addEditDepartmentWindow.ShowDialog() ?? false)
                        SetGrid(DataGridDepartments, _dbContext.departments.Include(d => d.Employees));
                }
                else
                {
                    AddEditDepartmentWindow addEditDepartmentWindow = new AddEditDepartmentWindow(_dbContext);
                    if (addEditDepartmentWindow.ShowDialog() ?? false)
                        SetGrid(DataGridDepartments, _dbContext.departments.Include(d => d.Employees));
                }
            }
            else
            {
                if (DataGridDepartments.SelectedItem != null &&
                DataGridDepartments.SelectedItem is Employee emp)
                {
                    AddEditEmployeeWindow addEditemployeeWindow = new AddEditEmployeeWindow(_dbContext, emp);
                    if (addEditemployeeWindow.ShowDialog() ?? false)
                        SetGrid(DataGridDepartments, _dbContext.employees.ToList());
                }
                else
                {
                    AddEditEmployeeWindow addEditemployeeWindow = new AddEditEmployeeWindow(_dbContext);
                    if (addEditemployeeWindow.ShowDialog() ?? false)
                        SetGrid(DataGridDepartments, _dbContext.employees.ToList());
                }
            }
        }

        private void MainDeleteClick(object sender, RoutedEventArgs e)
        {
            if(DataGridDepartments.SelectedItem != null && DataGridDepartments.SelectedItem is Department de)
            {
                _dbContext.departments.Remove(de);
                _dbContext.SaveChanges();
                SetGrid(DataGridDepartments, _dbContext.departments.Include(e => e.Employees));
            }
            else if(DataGridDepartments.SelectedItem != null && DataGridDepartments.SelectedItem is Employee emp)
            {
                _dbContext.employees.Remove(emp);
                _dbContext.SaveChanges();
                SetGrid(DataGridDepartments, _dbContext.employees.ToList());
            }
        }
        private void SeeAllEmployeesClick(object sender, RoutedEventArgs e)
        {
            _isDepartments = !_isDepartments;
            if (_isDepartments)
            {
                SetGrid(DataGridDepartments, _dbContext.departments.Include(d => d.Employees));
                AllEmployees.Content = "See All Employees";
                AddEditDepartmentButton.Content = "Add Or Edit Department";
                DeleteDepartmentButton.Content = "Delete Department";
            }
            else
            {
                SetGrid(DataGridDepartments, _dbContext.employees.ToList());
                AllEmployees.Content = "See All Departments";
                AddEditDepartmentButton.Content = "Add Or Edit Employee";
                DeleteDepartmentButton.Content = "Delete Employee";
            }
        }
    }
}
