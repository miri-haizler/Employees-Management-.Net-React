using Employee.Core.Entities;

namespace Employee.API.Models
{
    public class RoleEmployeePostModel
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public DateTime StartDateOfJob { get; set; }
        public bool IsManagerial { get; set; }
        public bool IsActive { get; set; }
    }
}
