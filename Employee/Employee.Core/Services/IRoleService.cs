using Employee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> GetRoleByIdAsync(int id);
        Task<Role> AddRoleAsync(Role role);
        Task<Role?>  UpdateRoleAsync(int id, Role role);
        Task<Role?> DeleteRoleAsync(int id);
       
    }
}
