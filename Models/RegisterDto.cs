using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class RegisterDto
    {

        [Key]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string UserName { get; set; } = default!;

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [StringLength(60)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;







    }
}
