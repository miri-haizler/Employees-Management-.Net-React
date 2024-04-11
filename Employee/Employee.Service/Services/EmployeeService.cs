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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepostitory _employeeRepository;
        public EmployeeService(IEmployeeRepostitory employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDetails>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllEmployeesAsync();
        }

        public async Task<EmployeeDetails?> GetEmployeeByIdAsync(string tz)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(tz);
        }

        public async Task<bool> AddEmployeeAsync(EmployeeDetails employee)
        {
            foreach (var item in employee.RolesEmployee)
            {
                if (item.StartDateOfJob < employee.StartDate)
                {
                    throw new ArgumentException("Start Date must be after begin working.");
                }
            }

            if (employee.Tz.Length < 9 || employee.Tz.Length > 9)
            {
                throw new ArgumentException("Employee Tz must be exactly 9 Digits. ");
            }
            return await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task<EmployeeDetails?> UpdateEmployeeAsync(string tz, EmployeeDetails employee)
        {
            if (employee.RolesEmployee != null)
            {
                foreach (var item in employee.RolesEmployee)
                {
                    if (item.StartDateOfJob < employee.StartDate)
                    {
                        throw new ArgumentException("Start Date must be after begin working.");
                    }
                }
            }
            if (employee.Tz.Length < 9 || employee.Tz.Length > 9)
            {
                throw new ArgumentException("Employee Tz must be exactly 9 Digits. ");
            }
            return await _employeeRepository.UpdateEmployeeAsync(tz, employee);
        }

        public async Task<EmployeeDetails?> DeleteEmployeeAsync(string tz)
        {
            if (tz.Length == 0)
            {
                throw new ArgumentException("Tz can't be null.");
            }
            return await _employeeRepository.DeleteEmployeeAsync(tz);
        }
    }
}
