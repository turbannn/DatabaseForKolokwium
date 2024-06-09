using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolokwium.Model
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string City { get; set; } = null!;
        public string Address { get; set; } = null!;
        [Hide, ForeignKey("DepartmentIdFK")]
        public IList<Employee> Employees { get; set; } = null!;
        public string AllEmployees => Employees.Count > 0 ? string.Join("; ", Employees) : "";
    }
}
