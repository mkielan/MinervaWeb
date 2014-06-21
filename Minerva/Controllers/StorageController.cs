using Minerva.Models.Web.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minerva.Resources;
using Minerva.Repositories;
using Minerva.Entities;

namespace Minerva.Controllers
{
    [Authorize]
    public class StorageController : Controller
    {
        private DiskStructureRepository _repository;

        public StorageController()
        {
            _repository = new DiskStructureRepository(new MinervaDbContext());
        }

        // GET: Storage
        public ActionResult Index(long? id)
        {
            ViewBag.Title = Layout.YourStorage;
            ViewBag.Editing = true;
            ViewBag.CurrentId = id.HasValue ? id : 1;

            var ret = _repository
                .FindBy(d => 
                    (!id.HasValue && d.Parent.Id == 1 
                    || id.HasValue && id.Value == d.Parent.Id)
                    && d.DeletedTime == null
                    )
                .Select(d => new GridItem {
                    Id = d.Id,
                    Name = d.Name,
                    Type = d.File == null ? ItemType.Directory: ItemType.File,
                    LastModification = d.ModificationTime
                });

            return View("Storage", ret.ToList());
        }
    }
}