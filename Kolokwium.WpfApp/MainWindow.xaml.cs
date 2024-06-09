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
        }
        private void AddEditDepartmentClick(object sender, RoutedEventArgs e)
        {
            if(DataGridDepartments.SelectedItem != null && DataGridDepartments.SelectedItem is Department d)
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

        private void DeleteEmployeeClick(object sender, RoutedEventArgs e)
        {
            if(DataGridDepartments.SelectedItem != null && DataGridDepartments.SelectedItem is Department de)
            {
                _dbContext.departments.Remove(de);
                _dbContext.SaveChanges();
                SetGrid(DataGridDepartments, _dbContext.departments.Include(e => e.Employees));
            }
        }

        private void DepartmentDetailsClick(object sender, RoutedEventArgs e)
        {
            if(DataGridDepartments.SelectedItem != null && DataGridDepartments.SelectedItem is Department de)
            {
                DepartmentDetailsWindow depDetailsWindow = new DepartmentDetailsWindow(_dbContext, de);
                depDetailsWindow.ShowDialog();
            }
        }
    }
}
