using Kolokwium.DAL.EF;
using Kolokwium.Model;
using Microsoft.EntityFrameworkCore;
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

namespace Kolokwium.WpfApp
{
    /// <summary>
    /// Логика взаимодействия для DepartmentDetailsWindow.xaml
    /// </summary>
    public partial class DepartmentDetailsWindow : Window
    {
        private readonly ApplicationDbContext _dbContext;
        private Department _department;
        public DepartmentDetailsWindow(ApplicationDbContext DBcontext, Department dep)
        {
            _dbContext = DBcontext;
            InitializeComponent();
            _department = dep;
        }
        private void WindowLoadedDetails(object sender, RoutedEventArgs e)
        {
            MainWindow.SetGrid(DataGridEmployees, _department.Employees);
        }
        private void BackClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
