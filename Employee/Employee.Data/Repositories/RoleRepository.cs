using Employee.Core.Entities;
using Employee.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync() => await _context.Roles.Include(e => e.EmployeesRole).ThenInclude(e => e.Employees).ToListAsync();

        public async Task<Role?> GetRoleByIdAsync(int id) => await _context.Roles.FindAsync(id);

        public async Task<Role> AddRoleAsync(Role role)
        {
            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task<Role?> UpdateRoleAsync(int id, Role role)
        {
            var updateRole = _context.Roles.ToList().Find(u => u.Id == id);
            if (updateRole != null)
            {
                updateRole.Name = role.Name;
                //updateRole.IsManagerial = role.IsManagerial;
                await _context.SaveChangesAsync();
            }
            return updateRole;
        }

        public async Task<Role?> DeleteRoleAsync(int id)
        {
            var role = await GetRoleByIdAsync(id);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
            return role;
        }
    }
}