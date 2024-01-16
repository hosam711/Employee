using Microsoft.EntityFrameworkCore;
using Emp.Models.Domain;
using Emp.Models;

namespace Emp.Data
{
    public class Db_context : DbContext
    {
        public Db_context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee>? Employees { get; set; }
        public DbSet<TimePeriod>? TimePeriod { get; set; }
        public DbSet<AttendanceView>? AttendanceView { get; set; }
        public DbSet<DifferenceinMinutes>? DifferenceInMinutes { get; set; }
        public DbSet<NetComViewModel>? NetComViewModel { get; set; } 


    }
}
