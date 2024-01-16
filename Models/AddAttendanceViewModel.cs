namespace Emp.Models
{
    public class AddAttendanceViewModel
    {

        public string? PeriodId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }       
        public DateTime? TimeIn { get; set; }
        public DateTime? TimeOut { get; set; }
        public string? Name { get; set;}
    }
}
