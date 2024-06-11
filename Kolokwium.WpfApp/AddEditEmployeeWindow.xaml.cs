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
    /// Логика взаимодействия для AddEditEmployeeWindow.xaml
    /// </summary>
    public partial class AddEditEmployeeWindow : Window
    {
        private readonly ApplicationDbContext _dbcontext;
        private Employee? _employee;
        public AddEditEmployeeWindow(ApplicationDbContext DBContext, Employee? emp = null)
        {
            _dbcontext = DBContext;
            InitializeComponent();
            _employee = emp;
        }

        private void AddEmployeeClick(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(ENameBox.Text) &&
                !string.IsNullOrEmpty(SurNameBox.Text) &&
                !string.IsNullOrEmpty(DepartNameBox.Text) &&
                DatePickerEmployee.SelectedDate != null)
            {

                if(_employee == null)
                {
                    _employee = new Employee();
                    _employee.Name = ENameBox.Text;
                    _employee.SurName = SurNameBox.Text;
                    _employee.BirthDate = DatePickerEmployee.SelectedDate;
                    _employee.YearsOfEmployment = 0;
                    _dbcontext.departments.First(e => e.Name == DepartNameBox.Text).Employees.Add(_employee);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    _employee.Name = ENameBox.Text;
                    _employee.SurName = SurNameBox.Text;
                    _employee.BirthDate = DatePickerEmployee.SelectedDate;
                    if(YearsOfEmploymentBox.Text != null)
                    {
                        int.TryParse(YearsOfEmploymentBox.Text, out int ww);
                        _employee.YearsOfEmployment = ww;
                    }
                    _dbcontext.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Error occured");
            }
            DialogResult = true;
        }
    }
}
