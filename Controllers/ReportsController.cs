using Emp.Data;
using Microsoft.AspNetCore.Mvc;
using Emp.Models;
using Emp.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.IO;
using AspNetCore.Reporting;
using System.Data;
using Microsoft.Data.SqlClient.Server;
using Microsoft.SqlServer.Server;
using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using System.Dynamic;

namespace Emp.Controllers
{
    public class ReportsController : Controller

    {
        private readonly Db_context Db_context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportsController(IWebHostEnvironment webHostEnvironment, Db_context db_Context)
        {
            this._webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            this.Db_context = db_Context;
        }
        //[Route("Attendance")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            

            var employees = await Db_context.Employees.ToListAsync();
            var empList = Db_context.DifferenceInMinutes.ToList();

            //empList.Insert(0, new Models.DifferenceinMinutes { PhoneNumber = "0", Name = "Select" });

            //ViewBag.ListofEmp = empList;
            ViewBag.Date1 = DateTime.Now.AddMonths(-1);
            ViewBag.Date2 = DateTime.Now;    
            //ViewData["todaysdate"] = DateTime.Today.AddMonths(-1).ToShortDateString();

            return View(employees);

        }
      
        [HttpPost]
        public ActionResult AtView(DateTime Date1, DateTime Date2, string PhoneNumber)
        {
            if (PhoneNumber == null)
            {
                ViewBag.Date1 = Date1;
                ViewBag.Date2 = Date2;

                var tpQuery = Db_context.NetComViewModel.Where(x => x.DateIn >= Date1)
                   .Where(x => x.DateIn <= Date2)
                   .OrderBy(x => x.Name)
                   .ThenBy(x => x.DateIn)
                   .ToList();
                
                return View("NetComReportViewAll", tpQuery);
            }
            else 
            { 
            ViewBag.Date1 = Date1;
            ViewBag.Date2 = Date2;

            ViewBag.PhoneNumber = PhoneNumber;


            var tpQuery = Db_context.NetComViewModel.Where(x => x.DateIn >= Date1)
                .Where(x => x.DateIn <= Date2)
                .Where(x => x.PhoneNumber == PhoneNumber)
                .OrderBy(x => x.DateIn).ToList();
            ViewBag.tpquery = tpQuery;
            ViewBag.Name = Db_context.NetComViewModel.Where(x => x.PhoneNumber == PhoneNumber).FirstOrDefault()?.Name;
            //calculate the sum for the employee
            var nsum = Db_context.NetComViewModel
               .Where(x => x.PhoneNumber == PhoneNumber)
               .Sum(x => x.NetCompansation);
            ViewBag.NetSum = $"{nsum:0.00}";
            return View("NetComReportView", tpQuery);
            }

        }


        [HttpPost]
        public ActionResult Print(DateTime Date1, DateTime Date2, string PhoneNumber)
        {

            if (PhoneNumber == null)
            {
                return RedirectToAction("Print", "AllEmpRep", new { Date1 = Date1, Date2 = Date2 });
            }
            else
            {

                string mimtype = "";
                int extension = 1;
                var pathToOne = $"{this._webHostEnvironment.WebRootPath}\\Reports\\EmpReport.rdlc";
                Dictionary<string, string> parameters = new();
                var employees = Db_context.NetComViewModel.Where(x => x.DateIn >= Date1)
                  .Where(x => x.DateIn <= Date2)
                  .Where(x => x.PhoneNumber == PhoneNumber)
                  .OrderBy(x => x.DateIn).ToList();


                var NetSum = Db_context.NetComViewModel
                    .Where(x => x.PhoneNumber == PhoneNumber)
                    .Sum(x => x.NetCompansation).ToString();

                string? EName = Db_context.NetComViewModel.Where(x => x.PhoneNumber == PhoneNumber).FirstOrDefault()?.Name;
                parameters.Clear();
                parameters.Add("EmpName", EName);
                parameters.Add("DateFrom", Date1.ToShortDateString());
                parameters.Add("DateTo", Date2.ToShortDateString());
                parameters.Add("NetSum", NetSum);

                LocalReport localReport = new((string)pathToOne);
                localReport.AddDataSource("DataSet1", employees);

                var result = localReport.Execute(RenderType.Pdf, extension, null, mimtype);
                return File(result.MainStream, "application/pdf");


                }




            }

        }
    
}