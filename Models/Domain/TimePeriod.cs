namespace Emp.Models.Domain
{
    public class TimePeriod
    {

        public Guid Id { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateIn { get; set; }
        public string? TimeIn { get; set; }
        public string? TimeOut { get; set;}
    }
}
