namespace Emp.Models
{
    public class UpdateAttendanceViewModel
    {
        public string PeriodId { get; set; }
        public string EmpId { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public string Name { get; set; }
        public string PhoneNumber {  get; set; }

    }
}
