using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models.DTOs
{
    public class UpdateEmployeeDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? Phone { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Salary { get; set; }

        public Guid? DepartmentId { get; set; }
    }
}
