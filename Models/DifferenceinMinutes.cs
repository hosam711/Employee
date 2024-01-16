namespace Emp.Models
{
    public class DifferenceinMinutes
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime DateIn { get; set; }
        public string? TimeIn { get; set; }
        public string? TimeOut { get; set; }
        public int? TimeDifferenceInHr { get; set; }
        public double? TimeDifferenceInMinutes { get; set;}
        public double Salary { get; set; }
    }
}
