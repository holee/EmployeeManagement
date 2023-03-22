using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Models
{
    public class RegisterDto
    {

        [Key]
        public Guid? Id { get; set; }
        [Remote("CheckUserName", "Accounts",ErrorMessage ="this username already in used.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage ="Please enter your email address.")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="UserName at least 5 character,but not exceed 100 characters.")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [MinLength(3)]
        public string Password { get; set; } = string.Empty;


        [Required]
        [DataType(DataType.Password)]
        [MinLength(3)]
        [Compare("Password",ErrorMessage ="Password is not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;







    }
}
