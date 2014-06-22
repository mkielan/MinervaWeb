using Minerva.Models.Web.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minerva.Resources;
using Minerva.Repositories;
using Minerva.Models.Web.Comment;
using Minerva.Entities;
using Minerva.Helpers;

namespace Minerva.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private IDiskStructureRepository<MinervaDbContext> _repository;

        public FileController()
        {
            var c = new MinervaDbContext();
            _repository = new DiskStructureRepository(c);
        }

        public ActionResult Show(int id)
        {
            return View();
        }

        public FileResult Download()
        {
            return null;
        }

        [HttpPost]
        public JsonResult Upload()
        {
            return Json(1);
        }
    }
}