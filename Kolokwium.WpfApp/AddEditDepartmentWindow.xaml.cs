using Kolokwium.DAL.EF;
using Kolokwium.Model;
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
    /// Логика взаимодействия для AddEditDepartmentWindow.xaml
    /// </summary>
    public partial class AddEditDepartmentWindow : Window
    {
        private readonly ApplicationDbContext _dbcontext;
        private Department? _department;
        public AddEditDepartmentWindow(ApplicationDbContext DBContext, Department? dep = null)
        {
            _dbcontext = DBContext;
            InitializeComponent();
            _department = dep;
        }

        private void AddDepartmentClick(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(NameBox.Text) &&
                !string.IsNullOrEmpty(CityBox.Text) &&
                !string.IsNullOrEmpty(AddressBox.Text))
            {
                if(_department == null)
                {
                    _department = new Department();
                    _department.Name = NameBox.Text;
                    _department.City = CityBox.Text;
                    _department.Address = AddressBox.Text;
                    _dbcontext.departments.Add(_department);
                    _dbcontext.SaveChanges();
                }
                else
                {
                    _department.Name = NameBox.Text;
                    _department.City = CityBox.Text;
                    _department.Address = AddressBox.Text;
                    _dbcontext.SaveChanges();
                }
            }
            else
            {
                MessageBox.Show("Something went wrong");
            }
            DialogResult = true;
        }
    }
}
