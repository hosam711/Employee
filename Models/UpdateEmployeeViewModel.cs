namespace Emp.Models
{
    public class UpdateEmployeeViewModel
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public double Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Department { get; set; }
        public string EmpId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
