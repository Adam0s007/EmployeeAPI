using System;

namespace EmployeeAdminPortal.Models.Responses
{
    public class EmployeeResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public decimal Salary { get; set; }
        public DepartmentResponse Department { get; set; } = null!;
    }
}
