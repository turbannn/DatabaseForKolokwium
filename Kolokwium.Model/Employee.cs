using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kolokwium.Model
{
    public class Employee
    {
        [Hide, NotMapped]
        private int _depid;
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string SurName { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public int YearsOfEmployment {  get; set; }
        [Hide]
        public Department Department { get; set; } = null!;
        [ForeignKey("Department")]
        public int DepartmentIdFK
        {
            get => Department?.Id ?? _depid;
            set => _depid = value;
        }

        public override string ToString()
        {
            return $"{Id}, {Name}";
        }
    }
}
