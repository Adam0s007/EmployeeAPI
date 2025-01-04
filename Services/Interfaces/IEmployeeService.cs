using EmployeeAdminPortal.Models.DTOs;
using EmployeeAdminPortal.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAdminPortal.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeResponse>> GetAllEmployeesAsync();
        Task<EmployeeResponse?> GetEmployeeByIdAsync(Guid id);
        Task<EmployeeResponse> AddEmployeeAsync(AddEmployeeDto addEmployeeDto);
        Task<EmployeeResponse?> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto);
        Task<bool> DeleteEmployeeAsync(Guid id);
    }
}
