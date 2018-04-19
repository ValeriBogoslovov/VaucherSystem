using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VaucherSystem.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult HandleError()
        {
            return View("Error");
        }
    }
}