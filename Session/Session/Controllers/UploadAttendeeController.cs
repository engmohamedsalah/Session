using Session.Service;
using Session.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Session.Controllers
{
    public class UploadAttendeeController : Controller
    {
        ISessionAttendeeService _sessionAttendeeService;

        public UploadAttendeeController(ISessionAttendeeService sessionAttendeeService)
        {
            _sessionAttendeeService = sessionAttendeeService;
        }
        // GET: UploadAttendee
        public ActionResult Index()
        {
            var model = new UploadAttendeeViewModel();
            //model.LogMessage = "";
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(UploadAttendeeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StringBuilder logMessage = new StringBuilder();
            string filePath = string.Empty;
            if (model.File != null)
            {
                string path = Server.MapPath("~/Uploads/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                filePath = path + Path.GetFileName(model.File.FileName);
                string extension = Path.GetExtension(model.File.FileName);
                model.File.SaveAs(filePath);
                if (extension != ".csv")
                {
                    logMessage.Append("Not CSV file");
                    ViewBag.LogMessage = logMessage.ToString();
                    return View(model);
                }
                else
                    logMessage.Append(_sessionAttendeeService.UploadAttendee(filePath));

            }
            else
                logMessage.Append("File is empty");

            ViewBag.LogMessage = logMessage.ToString();
            return View(model);
        }
    }
}