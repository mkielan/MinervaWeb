using Minerva.Models.Web.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minerva.Controllers
{
    [Authorize]
    public class ShareController : Controller
    {
        // GET: Share
        public ActionResult ForYou()
        {
            ViewBag.Title = "Shared for you";

            var model = new List<GridItem>();

            return View("Storage", model);
        }

        public ActionResult ByYou()
        {
            ViewBag.Title = "Shared by you";

            var model = new List<GridItem>();

            return View("Storage", model);
        }
    }
}