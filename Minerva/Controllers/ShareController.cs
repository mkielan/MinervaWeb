using Minerva.Models.Web.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minerva.Resources;

namespace Minerva.Controllers
{
    [Authorize]
    public class ShareController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("ForYou");
        }

        // GET: Share
        public ActionResult ForYou()
        {
            ViewBag.Title = Layout.SharedForYou;
            
            var model = new List<GridItem>();

            return View("Storage", model);
        }

        public ActionResult ByYou()
        {
            ViewBag.Title = Layout.SharedByYou;

            var model = new List<GridItem>();

            return View("Storage", model);
        }

    }
}