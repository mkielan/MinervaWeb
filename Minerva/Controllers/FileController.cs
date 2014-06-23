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
using System.Configuration;
using System.IO;
using System.Web.Hosting;

namespace Minerva.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        private IDiskStructureRepository<MinervaDbContext> _repository;

        private string _storagePath;
        
        public FileController()
        {
            var c = new MinervaDbContext();
            _repository = new DiskStructureRepository(c);
            _storagePath = HostingEnvironment.MapPath(ConfigurationSettings.AppSettings["FilesStoragePath"]);
        }

        public ActionResult Show(int id)
        {
            return View();
        }

        public FileResult Download(int id)
        {
            var file = DiskStructureHelper.GetFile<MinervaDbContext>(_repository, id);

            if (file == null) return null;

            return File(
                _storagePath + "\\" + id, 
                FileHelper.ExtensionToMimetype(file.Name.Split('.').Last()), 
                file.Name
                );
        }

        [HttpPost]
        public JsonResult Upload(int id)
        {
            var file = DiskStructureHelper.GetFile<MinervaDbContext>(_repository, id);

            if (file == null) return Json(false);

            var ret = FileHelper.Save(this.Request, _storagePath + "//" + id);
            return Json(ret);
        }
    }
}