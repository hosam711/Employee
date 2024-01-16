using Emp.Data;
using Emp.Models;
using Emp.Models.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using System.Threading;

namespace Emp.Controllers
{
    public class AttendanceControler : Controller
    {
        private readonly Db_context Db_context;

        public AttendanceControler(Db_context db_Context)
        {
            this.Db_context = db_Context;
        }
        [Route("Attendance")]
        [HttpGet]
        public async Task<IActionResult>  Index()
        {
            var tp = await Db_context.AttendanceView.ToListAsync();
            
            return View(tp);
            
        }
        public ActionResult SignIn()
        {
            string st= DateTime.Now.ToShortDateString();
            ViewData["todaysdate"] = DateTime.Today.ToString();
            var tpQuery = Db_context.AttendanceView.Where(x => x.DateIn.Equals(DateTime.Parse(st))); ;

            List<Emp.Models.AttendanceView> tpList = tpQuery.ToList();

            return View(tpList);
        }

        [HttpPost]
        public async Task<IActionResult> Save(string searchPhone, string searchDateIn)
        {
            var tp = Db_context.TimePeriod.Where(x => x.PhoneNumber == searchPhone)
                                                             .Where(x => x.DateIn.Equals(DateTime.Parse(searchDateIn)))
                                                             .FirstOrDefault();
            if (tp != null)
            {
                return RedirectToAction("SignIn");
            }
                var timepn = new TimePeriod()
            {
                Id = Guid.NewGuid(),
                PhoneNumber = searchPhone,
                DateIn = DateTime.Today,
                TimeIn = DateTime.Now.ToShortTimeString(),
                TimeOut = null,
            };
            await Db_context.TimePeriod.AddAsync(timepn);
            await Db_context.SaveChangesAsync();
            return RedirectToAction("SignIn");

        }
        public ActionResult SignOut(String PhoneNumber)
        {
            string st = DateTime.Now.ToShortDateString(); 
            ViewData["todaysdate"] = DateTime.Today.ToString();
            var tpQuery = Db_context.AttendanceView.Where(x => x.DateIn.Equals(DateTime.Parse(st))); ;
            List<Emp.Models.AttendanceView> tpList = tpQuery.ToList();
            return View(tpList);
        }

        [HttpPost]
        public IActionResult Update(string searchPhone, string searchDateIn)
        {

            var tp = Db_context.TimePeriod.Where(x => x.PhoneNumber == searchPhone)
                                                             .Where(x => x.DateIn.Equals(DateTime.Parse(searchDateIn)))
                                                             .FirstOrDefault();
            if(tp != null)
            {
                tp.TimeOut = DateTime.Now.ToShortTimeString();
                Db_context.SaveChanges();
            
            }
            return RedirectToAction("SignOut");
            
        }
    }
}
