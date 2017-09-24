using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace biodata.Controllers
{
    public class CustomErrorController : Controller
    {
        [HandleError]
        public ActionResult Error()
        {
            return View();
        }

    }
}
