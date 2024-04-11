using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Entities
{
    public enum Gender { Male = 0 , Female   }
    public class EmployeeDetails
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Tz { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime DateOfBirth  { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public List<RoleEmployee>? RolesEmployee { get; set; }

    }
}