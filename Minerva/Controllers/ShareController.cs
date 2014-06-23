using Minerva.Models.Web.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minerva.Resources;
using Minerva.Entities;
using Minerva.Repositories;
using Minerva.Helpers;

namespace Minerva.Controllers
{
    [Authorize]
    public class ShareController : Controller
    {
        private IDiskStructureRepository<MinervaDbContext> _repository;

        public ShareController()
        {
            _repository = new DiskStructureRepository(new MinervaDbContext());
        }

        // GET: Share
        public ActionResult ForYou()
        {
            ViewBag.Title = Layout.SharedForYou;

            var model = DiskStructureHelper.FindShared(
                _repository,
                User.Identity.Name,
                DiskItemType.All,
                PrivilageToDiskItemType.Shared
                ).AsEnumerable()
                .Select(d => new GridItem
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = d.File == null ? ItemType.Directory : ItemType.File,
                    LastModification = d.ModificationTime
                }).ToList();

            return View("Storage", model);
        }

        public ActionResult ByYou()
        {
            ViewBag.Title = Layout.SharedByYou;

            var model = DiskStructureHelper.FindShared(
                _repository, 
                User.Identity.Name, 
                DiskItemType.All)
                .AsEnumerable()
                .Select(d => new GridItem
                {
                    Id = d.Id,
                    Name = d.Name,
                    Type = d.File == null ? ItemType.Directory : ItemType.File,
                    LastModification = d.ModificationTime
                }).ToList();

            return View("Storage", model);
        }
    }
}