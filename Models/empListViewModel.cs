using Emp.Models.Domain;

namespace Emp.Models
{
    public class empListViewModel
    {
        public IEnumerable<DifferenceinMinutes> DifferenceinMinutesViewModel { get; set; }
        public IEnumerable<Employee> EmployeeViewModel { get; set; }
    }
}
