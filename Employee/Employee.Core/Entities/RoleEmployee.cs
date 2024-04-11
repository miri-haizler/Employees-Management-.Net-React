using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Entities
{
    public class RoleEmployee
    {
        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        public Role? Roles { get; set; }
        public int EmployeeDetailsId { get; set; }
        public EmployeeDetails? Employees { get; set; }
        public DateTime StartDateOfJob { get; set; }
        public bool IsManagerial { get; set; }
        public bool IsActive { get; set; }

    }
}