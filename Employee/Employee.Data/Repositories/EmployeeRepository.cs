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
    public class EmployeeRepository : IEmployeeRepostitory
    {
        private readonly DataContext _context;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EmployeeDetails>> GetAllEmployeesAsync()
        {
            return await _context.Employees.Where(employee => employee.IsActive)
            .Include(e => e.RolesEmployee).ToListAsync();
        }

        public async Task<EmployeeDetails?> GetEmployeeByIdAsync(string tz)
        {
            return await _context.Employees.Include(e => e.RolesEmployee).FirstOrDefaultAsync(e => e.Tz == tz);
        }

        public async Task<bool> AddEmployeeAsync(EmployeeDetails employee)
        {
            try
            {
                _context.Employees.Add(employee);
                var isSuccess = await _context.SaveChangesAsync();
                return isSuccess > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<EmployeeDetails?> UpdateEmployeeAsync(string tz, EmployeeDetails employee)
        {
            var updateEmployee = _context.Employees.ToList().Find(u => u.Tz == tz);
            if (updateEmployee != null)
            {
                updateEmployee.FirstName = employee.FirstName;
                updateEmployee.LastName = employee.LastName;
                updateEmployee.Tz = employee.Tz;
                updateEmployee.DateOfBirth = employee.DateOfBirth;
                updateEmployee.StartDate = employee.StartDate;
                updateEmployee.Gender = employee.Gender;
                updateEmployee.IsActive = employee.IsActive;
                updateEmployee.RolesEmployee = employee.RolesEmployee;
                await _context.SaveChangesAsync();
            }
            return updateEmployee;
        }

        public async Task<EmployeeDetails?> DeleteEmployeeAsync(string tz)
        {
            var emp = await GetEmployeeByIdAsync(tz);
            if (emp != null)
            {
                emp.IsActive = false;
                await _context.SaveChangesAsync();
            }
            return emp;
        }
    }
}
