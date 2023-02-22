namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public int? DepartmetId { get; set; } 
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Gender { get; set; } = default!;
        public string Address { get; set; } =string.Empty;
    }
}
