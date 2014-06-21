using Minerva.Entities;
using Minerva.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Minerva.Controllers
{
    public class DiskItemController : Controller
    {
        private IDiskStructureRepository<MinervaDbContext> _repository;

        public DiskItemController()
        {
            _repository = new DiskStructureRepository(new MinervaDbContext());
        }

        public JsonResult Delete(int id)
        {
            var item = _repository.FindBy(ds => ds.Id == id).First();
            var username = User.Identity.Name;

            if (item.CreatedBy.UserName == username)
            {
                item.DeletedBy = _repository.Context.Users.First(u => u.UserName == username);

                _repository.Remove(item);
                _repository.Save();

                return Json(true);
            }

            return Json(false);
        }

        public JsonResult ShareWith(int id, string username)
        {


            return Json(true);
        }
    }
}