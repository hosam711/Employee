using Emp.Data;
using Emp.Models;
using Emp.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Emp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly Db_context Db_context;

        public EmployeesController(Db_context db_Context)
        {
            this.Db_context = db_Context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await Db_context.Employees.ToListAsync();
            return View(employees);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(UpdateEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                EmpId = addEmployeeRequest.EmpId,
                PhoneNumber = addEmployeeRequest.PhoneNumber,
            };
            await Db_context.Employees.AddAsync(employee);
            await Db_context.SaveChangesAsync();
            return RedirectToAction("Index");


        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var employee = await Db_context.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                    EmpId = employee.EmpId,
                    PhoneNumber = employee.PhoneNumber,

                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<ActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await Db_context.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                employee.EmpId = model.EmpId; 
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth; ;
                employee.Department = model.Department;
                employee.EmpId = model.EmpId;
                await Db_context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await Db_context.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                Db_context.Employees.Remove(employee);
                await Db_context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
