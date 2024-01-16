using Emp.Data;
using Emp.Models;
using Emp.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Emp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Db_context Db_context;
        public HomeController(ILogger<HomeController> logger , Db_context db_Context)
        {
            _logger = logger;
            this.Db_context = db_Context;
        }
       



        [HttpGet]
        public IActionResult Index()
        {
           // var employees = await Db_context.Employees.ToListAsync();
            return View();

        }

        
        
        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        //public IActionResult Search()
        //{
        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Search(String name, Guid id, String empid)
        {

            if (empid != null)
            {
                var employee = await Db_context.Employees.FirstOrDefaultAsync(x => x.EmpId == empid);
                if (employee != null)
                {
                    var viewModel = new UpdateEmployeeViewModel()
                    {
                        Id = employee.Id,
                        Name = employee.Name,
                        Email = employee.Email,
                        Department = employee.Department,
                        EmpId = employee.EmpId,
                    };
                    return await Task.Run(() => View("Search", viewModel));
                }
            }
            else if (name != null)
            {
                var employee1 = await Db_context.Employees.FirstOrDefaultAsync(x => x.Name == name);
                if (employee1 != null)
                {
                    var viewModel = new UpdateEmployeeViewModel()
                    {
                        Id = employee1.Id,
                        Name = employee1.Name,
                        Email = employee1.Email,

                        Department = employee1.Department,

                        EmpId = employee1.EmpId,

                    };
                    return await Task.Run(() => View("Search", viewModel));
                }



            }
            else 
            {
                var employee2 = await Db_context.Employees.FirstOrDefaultAsync(x => x.Id == id);
                if (employee2 != null)
                {
                    var viewModel = new UpdateEmployeeViewModel()
                    {
                        Id = employee2.Id,
                        Name = employee2.Name,
                        Email = employee2.Email,

                        Department = employee2.Department,

                        EmpId = employee2.EmpId,
                        PhoneNumber = employee2.PhoneNumber,

                    };
                    return await Task.Run(() => View("Search", viewModel));
                }
            }


            //return RedirectToRoute("Employees/index");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(string PhoneNumber)
        {

            var timep = new TimePeriod()
            {
                Id = Guid.NewGuid(),
                PhoneNumber = PhoneNumber,
                DateIn = DateTime.Today,
                TimeIn = DateTime.Now.ToShortTimeString(),
       

            };
            await Db_context.TimePeriod.AddAsync(timep);
            await Db_context.SaveChangesAsync();
           
            return RedirectToAction("SignIn");
        }
         [HttpGet]
        public async Task <ActionResult> SignIn()
        {
            var updateAttendance = await Db_context.AttendanceView.ToListAsync();
            return View(updateAttendance);

        }
       


        public IActionResult Attendance()
        {
            return RedirectToAction("Index","Attendance" );
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}