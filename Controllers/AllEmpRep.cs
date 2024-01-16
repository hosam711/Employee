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
    public class AllEmpRep : Controller
    {

        private readonly Db_context Db_context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AllEmpRep(IWebHostEnvironment webHostEnvironment, Db_context db_Context)
        {
            this._webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            this.Db_context = db_Context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Print(DateTime Date1, DateTime Date2)
        {


            string mimtype = "";
            int extension = 1;
            var pathToOne = $"{this._webHostEnvironment.WebRootPath}\\Reports\\AllEmpReport.rdlc";
            Dictionary<string, string> parameters = new();


            var employees = Db_context.NetComViewModel.Where(x => x.DateIn >= Date1)
                    .Where(x => x.DateIn <= Date2)
                    .OrderBy(x => x.DateIn)
                    .ToList();

            parameters.Clear();
            parameters.Add("DateFrom", Date1.ToShortDateString());
            parameters.Add("DateTo", Date2.ToShortDateString());


            LocalReport localReport1 = new((string)pathToOne);
            localReport1.AddDataSource("DataSet1", employees);

                var result = localReport1.Execute(RenderType.Pdf, extension, null, mimtype);
                return File(result.MainStream, "application/pdf");
         

        }
    }
}
