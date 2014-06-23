using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Minerva.Repositories;
using Minerva.Entities;
using Api = Minerva.Models.Api;
using Minerva.Entities.Sources;
using Minerva.Helpers;
using Minerva.Models.Web.Share;

namespace Minerva.Controllers
{
    [Authorize]
    public class DiskItemController : Controller
    {
        private IDiskStructureRepository<MinervaDbContext> _repository;

        public DiskItemController()
        {
            _repository = new DiskStructureRepository(new MinervaDbContext());
        }

        [HttpPost]
        public JsonResult AddFile(Api.File.Add model)
        {
            if (ModelState.IsValid)
            {
                var parent = _repository.FindBy(p => p.Id == model.ParentId).First();

                var diskStructure = new DiskStructure()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Parent = parent,
                    CreatedBy = _repository.Context.Users.First(u => u.UserName == User.Identity.Name),
                };

                var tmp = diskStructure.Name.Split('.');

                _repository.Add(diskStructure);
                var f = new File
                {
                    DiskStructureId =
                        diskStructure.Id,
                    DiskStructure = diskStructure,
                    Extension = tmp.Length > 1 ? tmp.Last() : null
                };

                _repository.Context.Files.Add(f);
                _repository.Save();

                return Json(diskStructure.Id, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        [HttpPost]
        public JsonResult AddDirectory(Api.Directory.Add model)
        {
            if (ModelState.IsValid)
            {
                var parent = _repository.FindBy(p => p.Id == model.ParentId).First();

                var dir = new DiskStructure()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Parent = parent,
                    CreatedBy = _repository.Context.Users.First(u => u.UserName == User.Identity.Name)
                };

                _repository.Add(dir);
                _repository.Save();

                return Json(dir.Id, JsonRequestBehavior.AllowGet);
            }

            return null;
        }

        [HttpPost]
        public JsonResult EditFile(Api.File.Edit model)
        {
            if (ModelState.IsValid)
            {

            }
            return null;
        }

        [HttpPost]
        public JsonResult EditDirectory(Api.Directory.Edit model)
        {
            if (ModelState.IsValid)
            {

            }
            return null;
        }

        [HttpGet]
        public JsonResult ViewFile(int id)
        {
            var file = _repository.FindBy(f => f.Id == id).First();
            return Json(ObjectConverter.FileToApiView(file));
        }

        [HttpGet]
        public JsonResult ViewDirectory(int id)
        {
            var dir = _repository.FindBy(d => d.Id == id).First();
            return Json(ObjectConverter.DirToApiView(dir));
        }

        [HttpPost]
        public JsonResult Delete(int[] id)
        {
            var user = _repository.Context.Users.First(u => u.UserName == User.Identity.Name);
            var items = _repository.FindBy(i => id.Contains(i.Id));

            foreach (var item in items)
            {
                item.DeletedBy = user;
                _repository.Remove(item);
            }

            _repository.Save();

            return Json(true);
        }

        [HttpPost]
        public JsonResult Share(ShareModel model)
        {
            var usersString = model.Users();
            var users = _repository.Context.Users.Where(u => usersString.Contains(u.UserName));
            var count = 0;
            try
            {
                count = users.Count();
            }
            catch
            {
            }
            
            if (usersString.Length == 0 || count == 0)
            {
                ModelState["Usernames"].Errors.Add(Resources.Validation.UsernamesNotFound);
                
                return Json(ModelState.ToSerializedDictionary());
            }

            if (!ModelState.IsValid)
            {
                return Json(ModelState.ToSerializedDictionary());
            }
            
            var user = _repository.Context.Users.First(u => u.UserName == User.Identity.Name);
            var items = _repository.FindBy(i => model.DiskItemIds.Contains(i.Id));
            
            foreach (var item in items)
            {
                DiskStructureHelper.ShareWith(_repository, item, usersString);
            }

            _repository.Save();


            return Json(true);
        }
    }
}