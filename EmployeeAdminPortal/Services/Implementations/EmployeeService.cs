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
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedResult<EmployeeResponse>> GetAllEmployeesAsync(int pageNumber, int pageSize)
        {
            var totalRecords = await _dbContext.Employees.CountAsync();

            var employees = await _dbContext.Employees
                .Include(e => e.Department)
                .OrderBy(e => e.Name) // Optional: Order by a specific field
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var employeeResponses = employees.Select(e => new EmployeeResponse
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                Salary = e.Salary,
                Department = e.Department != null ? new DepartmentResponse
                {
                    Id = e.Department.Id,
                    Name = e.Department.Name
                } : null
            });

            return new PagedResult<EmployeeResponse>
            {
                Items = employeeResponses,
                TotalRecords = totalRecords,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }

        public async Task<EmployeeResponse?> GetEmployeeByIdAsync(Guid id)
        {
            var e = await _dbContext.Employees
                .Include(emp => emp.Department)
                .FirstOrDefaultAsync(emp => emp.Id == id);

            if (e == null)
            {
                return null;
            }

            return new EmployeeResponse
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Phone = e.Phone,
                Salary = e.Salary,
                Department = e.Department != null ? new DepartmentResponse
                {
                    Id = e.Department.Id,
                    Name = e.Department.Name
                } : null
            };
        }

        public async Task<EmployeeResponse> AddEmployeeAsync(AddEmployeeDto addEmployeeDto)
        {
            Department? department = null;
            if (addEmployeeDto.DepartmentId.HasValue)
            {
                department = await _dbContext.Departments.FindAsync(addEmployeeDto.DepartmentId.Value);
                if (department == null)
                {
                    throw new ArgumentException("Invalid DepartmentId.");
                }
            }

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                Salary = addEmployeeDto.Salary,
                DepartmentId = addEmployeeDto.DepartmentId
            };

            _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();

            return new EmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                Department = department != null ? new DepartmentResponse
                {
                    Id = department.Id,
                    Name = department.Name
                } : null
            };
        }

        public async Task<EmployeeResponse?> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateEmployeeDto)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return null;
            }

            Department? department = null;
            if (updateEmployeeDto.DepartmentId.HasValue)
            {
                department = await _dbContext.Departments.FindAsync(updateEmployeeDto.DepartmentId.Value);
                if (department == null)
                {
                    throw new ArgumentException("Invalid DepartmentId.");
                }
            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.Salary = updateEmployeeDto.Salary;
            employee.DepartmentId = updateEmployeeDto.DepartmentId;

            _dbContext.Employees.Update(employee);
            await _dbContext.SaveChangesAsync();

            return new EmployeeResponse
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Salary = employee.Salary,
                Department = department != null ? new DepartmentResponse
                {
                    Id = department.Id,
                    Name = department.Name
                } : null
            };
        }

        public async Task<bool> DeleteEmployeeAsync(Guid id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return false;
            }

            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
