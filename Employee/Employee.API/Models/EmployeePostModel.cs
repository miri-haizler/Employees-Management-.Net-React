using Employee.Core.Entities;

namespace Employee.API.Models
{
    public class EmployeePostModel
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Tz { get; set; } = default!;
        public DateTime StartDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<RoleEmployeePostModel> RolesEmployee { get; set; } = default!;
    }
}
