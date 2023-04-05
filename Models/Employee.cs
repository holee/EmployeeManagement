namespace EmployeeManagement.Models
{

    public enum Dep
    {
        IT,
        Account,
        Marketing,
        Administrator,
        Consultant
    }

    public class Employee
    {
        public int EmpId { get; set; }
        public int? DepartmentId { get; set; } 
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string Gender { get; set; } = default!;
        public string Address { get; set; } =string.Empty;
    }
}
