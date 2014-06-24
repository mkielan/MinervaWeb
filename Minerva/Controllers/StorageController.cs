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
    public class StorageController : Controller
    {
        private IDiskStructureRepository<MinervaDbContext> _repository;
        private ICommentRepository<MinervaDbContext> _commentRepository;

        public StorageController()
        {
            var c = new MinervaDbContext();
            _repository = new DiskStructureRepository(c);
            _commentRepository = new CommentRepository(c);
        }

        // GET: Storage
        public ActionResult Index(int? id)
        {
            var itemId = id ?? 1;
            var item = _repository.FindBy(ds => ds.Id == itemId).First();

            if (item.File != null) throw new Exception("The item isn't directory!");

            #region załadowanie ViewBaga

            ViewBag.Editing = true;
            ViewBag.CurrentId = itemId;
            ViewBag.ParentId = item.Parent == null ? null : (int?)item.Parent.Id;
            ViewBag.Parents = DiskStructureHelper.GetBarecrumbParentFor(item);

            #endregion

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
                    LastModification = d.ModificationTime,
                    Creator = d.CreatedBy.UserName
                });

            ViewBag.Title = Resources.Layout.YourStorage;

            return View("Storage", ret.ToList());
        }

        // GET: Storage/Comments
        public ActionResult Comments(int? id)
        {
            var  itemId = id ?? 1;

            return View(new ChatModel
            {
                ItemId = itemId,
                UserName = User.Identity.Name,
                /*Messages = _commentRepository
                    .PrepareItemComments(itemId)
                    .Select(c => new Item {
                        Author = c.CreatedBy.UserName,
                        Body = c.Body,
                        SendTime = c.CreatedTime
                     }).ToList()*/
            });
        }

        [ChildActionOnly]
        public ActionResult CommentsChat(int id)
        {
            var model = new ChatModel
            {
                ItemId = id,
                UserName = User.Identity.Name
            };

            return (ActionResult)PartialView("_CommentsChat", model);
        }
    }
}