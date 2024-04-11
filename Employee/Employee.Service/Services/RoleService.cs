using Employee.Core.Entities;
using Employee.Core.Repositories;
using Employee.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Service.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return  _roleRepository.GetAllRolesAsync();
        }

        public Task<Role?> GetRoleByIdAsync(int id)
        {
            return _roleRepository.GetRoleByIdAsync(id);
        }

        public Task<Role> AddRoleAsync(Role role)
        {
            return _roleRepository.AddRoleAsync(role);
        }

        public Task<Role?> UpdateRoleAsync(int id, Role role)
        {
            return _roleRepository.UpdateRoleAsync(id, role);
        }


        public Task<Role?> DeleteRoleAsync(int id)
        {
          return  _roleRepository.DeleteRoleAsync(id);
        }
    }
}
