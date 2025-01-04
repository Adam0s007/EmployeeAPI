using System.ComponentModel.DataAnnotations;

namespace EmployeeAdminPortal.Models.DTOs
{
    public class DepartmentDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
