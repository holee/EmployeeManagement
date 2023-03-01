using Microsoft.Build.Execution;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class LoginDto
    {
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
