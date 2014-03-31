using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minevra.Areas.Api.Controllers
{
    public class HelpController : Controller
    {
        //
        // GET: /Api/Help/
        public ActionResult Index()
        {
            return View();
        }
	}
}