using Minerva.Entities;
using Minerva.Entities.Sources;
using Minerva.Repositories;
using IO = System.IO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using Minerva.Models.Api.File;
using Minerva.Helpers;
using Minerva.Infrastructure;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania plikami.
    /// </summary>
    [Authorize]
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        private IDiskStructureRepository<MinervaDbContext> _repository;
        private GenericRepository<MinervaDbContext, Tag> _tagRepository;
        private string _path;

        public FileController()
        {
            var context = new MinervaDbContext();
            _repository = new DiskStructureRepository(context);
            _tagRepository = new TagRepository(context);

            _path = HttpContext.Current.Server.MapPath(ConfigurationSettings.AppSettings["FilesStoragePath"]);
        }

        // GET api/file/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var username = User.Identity.Name;

            var file = _repository
                .FindBy(f => f.Id == id 
                    && f.File != null 
                    //&& f.CreatedBy.UserName == username
                    )
                .FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            return Ok(new View {
                Id = file.Id,
                Name = file.Name,
                Tags = file.Tags != null ? file.Tags.Select(t => t.Name).ToArray() : null,
                Url = "url", //todo
                Description = file.Description,
                Extension = file.File.Extension ?? "",
                Phone = file.CreatedBy != null && file.CreatedBy.Phone != null ?file.CreatedBy.Phone : "",
                Creator = file.CreatedBy != null ? file.CreatedBy.UserName : "",
                LastModificator = file.ModifiedBy != null ? file.ModifiedBy.UserName : ""
            });
        }

        // POST api/file
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]Add file)
        {
            DiskStructure parent = null;
            var username = User.Identity.Name;
            if (file.ParentId >= 0)
            {
                try
                {
                    var a = _repository.FindBy(
                        ds => ds.Id == file.ParentId 
                            && ds.File == null 
                            //&& ds.CreatedBy.UserName == User.Identity.Name
                            && ds.DeletedTime == null
                    );

                    parent = a.First();

                    var b = _repository.FindBy(ds => ds.Parent.Id == parent.Id 
                        && ds.Name == file.Name 
                        && ds.DeletedTime == null
                    ).Any();

                    if (b)
                    {
                        ModelState["ParentId"].Errors.Add("Item with this name already exist in this direcotry");
                    }
                }
                catch (ArgumentNullException exc)
                {
                    ModelState["ParentId"].Errors.Add(exc);
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _repository.Context.Users.First(u => u.UserName == username);
            var diskStructure = new DiskStructure
            {
                Name = file.Name,
                Description = file.Description,
                Parent = parent,
                CreatedBy = user,
                CreatedTime = DateTime.Now
            };

            var tmp = diskStructure.Name.Split('.');

            _repository.Add(diskStructure);
            var f = new File { DiskStructureId = 
                diskStructure.Id, 
                DiskStructure = diskStructure,
                Extension = tmp.Length > 1 ? tmp.Last() : null
            };

            #region dodanie tagów
            if (file.Tags != null)
            {
                if (diskStructure.Tags == null) diskStructure.Tags = new List<Tag>();

                foreach (var tag in file.Tags)
                {
                    var tagEntity = _repository.Context.Tags.FirstOrDefault(t => t.Name == tag);

                    if (tagEntity == null)
                    {
                        tagEntity = new Tag { Name = tag };
                        tagEntity.CreatedTime = DateTime.Now;
                        tagEntity.CreatedBy = user;
                        _repository.Context.Tags.Add(tagEntity);
                    }
                    diskStructure.Tags.Add(tagEntity);
                }
            }
            else
            {
                if (diskStructure.Tags != null) diskStructure.Tags.Clear();
            }
            #endregion

            _repository.Context.Files.Add(f);

            _repository.Save();

            return Ok(diskStructure.Id);
        }

        // PUT api/file/5
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]Edit file)
        {
            var username = User.Identity.Name;

            var b = _repository.FindBy(
                ds => ds.Parent.Id == id 
                    && ds.Name == file.Name 
                    && ds.DeletedTime == null
                ).Any();
            if (b)
            {
                ModelState["ParentId"].Errors.Add("Item with this name already exist in this direcotry");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = _repository.FindBy(c => c.Id == id).FirstOrDefault();
            if (com == null)
            {
                return NotFound();
            }

            var user = _repository.Context.Users.First(u => u.UserName == username);

            com.Name = file.Name;
            com.Description = file.Description;
            com.ModifiedBy = user;

            if(file.Tags != null) {
                if (com.Tags == null) com.Tags = new List<Tag>();

                foreach(var tag in file.Tags) 
                {
                    var tagEntity = _repository.Context.Tags.FirstOrDefault(t => t.Name == tag);

                    if (tagEntity == null)
                    {
                        tagEntity = new Tag { Name = tag };
                        tagEntity.CreatedTime = DateTime.Now;
                        tagEntity.CreatedBy = user;
                        _repository.Context.Tags.Add(tagEntity);
                    }
                    com.Tags.Add(tagEntity);
                }
            }
            else
            {
                if(com.Tags != null) com.Tags.Clear();
            }

            _repository.Edit(com);
            _repository.Save();

            return Ok(true);
        }

        // DELETE api/file/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var username = User.Identity.Name;

            var entity = _repository
                .FindBy(f => f.Id == id 
                    && f.File != null 
                    && f.DeletedTime == null
                    //&& f.CreatedBy.UserName == username
                    )
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            entity.DeletedBy = _repository.Context.Users.First(u => u.UserName == username);
            
            _repository.Remove(entity);
            _repository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET api/file/findbytag/black
        [HttpGet]
        public IEnumerable<View> FindByTag(string id)
        {
            var _tag = _repository.Context.Tags.Include("DiskStructures").Where(t => t.Name == id).FirstOrDefault();

            if(_tag != null) {
                return (
                    from t in _tag.DiskStructures 
                    where t.File != null
                        && t.DeletedTime == null
                        //&& t.CreatedBy.UserName == User.Identity.Name
                    select new View
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Tags = t.Tags != null ? t.Tags.Select(tt => tt.Name).ToArray() : null,
                        Url = "url", //todo
                        Description = t.Description,
                        Extension = t.File.Extension ?? "",
                        Phone = t.CreatedBy != null && t.CreatedBy.Phone != null ? t.CreatedBy.Phone : "",
                        Creator = t.CreatedBy != null ? t.CreatedBy.UserName : "",
                        LastModificator = t.ModifiedBy != null ? t.ModifiedBy.UserName : ""
                    }
                );
            }
            
            return null;
        }

        // GET api/file/GetSharedForMe
        [Route("SharedForMe")]
        public IEnumerable<View> GetSharedForMe()
        {
            var username = User.Identity.Name;
            
            var tmp = DiskStructureHelper.FindShared(
                _repository, 
                username, 
                DiskItemType.File, 
                PrivilageToDiskItemType.Shared
                );
            
            return ObjectConverter.ManyFilesToApiView(tmp);
        }

        // GET api/file/GetSharedByMe
        [Route("SharedByMe")]
        public IEnumerable<View> GetSheredByMe()
        {
            var username = User.Identity.Name;
            
            var tmp = DiskStructureHelper.FindShared(
                _repository, 
                username, 
                DiskItemType.File);
            
            return ObjectConverter.ManyFilesToApiView(tmp);
        }

        // GET api/file/id/share/Mariusz
        [HttpGet]
        [Route("{id:int}/share/{username}")]
        public async Task<IHttpActionResult> ShareWith(int id, string username)
        {
            var usernameCur = "Mariusz"; //User.Identity.Name

            try
            {
                var dir = _repository.FindBy(
                        d => d.Id == id
                            && d.DeletedTime == null
                            && d.File != null
                            //&& d.CreatedBy.UserName == usernameCur
                    ).First();

                var tmp = DiskStructureHelper.ShareWith(_repository, dir, username);
                _repository.Save();

                return Ok(true);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        // GET api/file/5/upload
        [HttpPost]
        [AllowAnonymous]
        [Route("{id:int}/upload")]
        public async Task<IHttpActionResult> Upload(int id)
        {
            var username = User.Identity.Name;
            if (!HaveAccessToFile(id, username))
            {
                return NotFound();
            }

            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count == 1)
            {
                var postedFile = httpRequest.Files[0];
                postedFile.SaveAs(_path + "//" + id);

                return Ok(true);
            }
            else
            {
                return BadRequest();
            }
        }

        // GET api/file/5/download
        [HttpGet]
        [AllowAnonymous]
        [Route("{id:int}/download")]
        public async Task<IHttpActionResult> Download(int id)
        {
            var username = User.Identity.Name;
            if (!HaveAccessToFile(id, username))
            {
                return NotFound();
            }

            return new FileActionResult(_path + "//" + id);
        }

        private bool HaveAccessToFile(int id, string username)
        {
            return true;/* _repository.FindBy(
                     f => f.DeletedTime == null
                         && f.Id == id
                         && f.File != null
                         && (
                         //f.CreatedBy.UserName == username
                         //|| 
                             f.AvailableFor.Select(a => a.User.UserName).Contains(username)
                         )
                 ).FirstOrDefault() != null;*/
        }
    }
}