using Employee.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Core.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDetails>> GetAllEmployeesAsync();
        Task<EmployeeDetails?> GetEmployeeByIdAsync(string tz);
        Task<bool> AddEmployeeAsync(EmployeeDetails employee);
        Task<EmployeeDetails?> UpdateEmployeeAsync(string tz, EmployeeDetails employee);
        Task<EmployeeDetails?> DeleteEmployeeAsync(string tz);
    }
}
