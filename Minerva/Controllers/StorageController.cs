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
    public class StorageController : Controller
    {
        // GET: Storage
        public ActionResult Index()
        {
            ViewBag.Title = Layout.YourStorage;
            ViewBag.Editing = true;

            var test = new List<GridItem>();
            test.AddRange(
                new [] { 
                    new GridItem {Id = 1, Name = "Test1", Type = ItemType.Directory },
                    new GridItem {Id = 2, Name = "Test2", Type = ItemType.Directory },
                    new GridItem {Id = 3, Name = "Test3", Type = ItemType.Directory },
                    new GridItem {Id = 4, Name = "File1", Type = ItemType.File, LastModification = DateTime.Now.AddHours(-15) },
                    new GridItem {Id = 5, Name = "File2", Type = ItemType.File, LastModification = DateTime.Now.AddHours(-25) },
                    new GridItem {Id = 6, Name = "File3", Type = ItemType.File, LastModification = DateTime.Now.AddHours(-3) }
                });

            return View("Storage", test);
        }
    }
}