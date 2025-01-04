using EmployeeAdminPortal.Models.DTOs;
using EmployeeAdminPortal.Models.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeAdminPortal.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentResponse>> GetAllDepartmentsAsync();
        Task<DepartmentResponse?> GetDepartmentByIdAsync(Guid id);
        Task<DepartmentResponse> AddDepartmentAsync(DepartmentDto departmentDto);
        Task<DepartmentResponse?> UpdateDepartmentAsync(Guid id, DepartmentDto departmentDto);
        Task<bool> DeleteDepartmentAsync(Guid id);
    }
}
