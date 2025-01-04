using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models.DTOs;
using EmployeeAdminPortal.Models.Entities;
using EmployeeAdminPortal.Models.Responses;
using EmployeeAdminPortal.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAdminPortal.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<DepartmentResponse>> GetAllDepartmentsAsync()
        {
            var departments = await _dbContext.Departments.ToListAsync();
            return departments.Select(d => new DepartmentResponse
            {
                Id = d.Id,
                Name = d.Name
            });
        }

        public async Task<DepartmentResponse?> GetDepartmentByIdAsync(Guid id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return null;
            }

            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public async Task<DepartmentResponse> AddDepartmentAsync(DepartmentDto departmentDto)
        {
            var department = new Department
            {
                Id = Guid.NewGuid(),
                Name = departmentDto.Name
            };

            _dbContext.Departments.Add(department);
            await _dbContext.SaveChangesAsync();

            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public async Task<DepartmentResponse?> UpdateDepartmentAsync(Guid id, DepartmentDto departmentDto)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return null;
            }

            department.Name = departmentDto.Name;
            _dbContext.Departments.Update(department);
            await _dbContext.SaveChangesAsync();

            return new DepartmentResponse
            {
                Id = department.Id,
                Name = department.Name
            };
        }

        public async Task<bool> DeleteDepartmentAsync(Guid id)
        {
            var department = await _dbContext.Departments.FindAsync(id);
            if (department == null)
            {
                return false;
            }

            _dbContext.Departments.Remove(department);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
