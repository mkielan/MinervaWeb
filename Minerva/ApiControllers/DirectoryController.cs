using Minerva.Entities;
using Minerva.Entities.Sources;
using Minerva.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using DirModels = Minerva.Models.Api.Directory;
using FileModels = Minerva.Models.Api.File;
using System.Collections;
using Minerva.Helpers;
using Minerva.Resources;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania katalogami.
    /// </summary>
    //[Authorize]
    [RoutePrefix("api/Directory")]
    public class DirectoryController : ApiController
    {
        private IDiskStructureRepository<MinervaDbContext> _repository;

        public DirectoryController()
        {
            var context = new MinervaDbContext();
            _repository = new DiskStructureRepository(context);
        }

        // GET: api/Directory
        /// <summary>
        /// Zwraca listę wszystkich folderów
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DirModels.View> Get()
        {
            var tmp = from d in _repository.GetAll()
                      where d.File == null
                        && d.DeletedBy == null
                        //&& d.CreatedBy.UserName == User.Identity.Name
                      select d;

            return ObjectConverter.ManyDirToApiView(tmp);
        }

        // GET: api/Directory/5
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var dir = _repository
                .FindBy(
                    d => 
                        d.Id == id
                        && d.File == null
                        && d.DeletedTime == null
                        //&& d.CreatedBy.UserName == User.Identity.Name
                ).FirstOrDefault();
            if (dir == null) return NotFound();

            return Ok(ObjectConverter.DirToApiView(dir));
        }

        // POST: api/Directory
        /// <summary>
        /// Dodaje katalog
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]DirModels.Add directory)
        {
            DiskStructure parent = null;
            var username = User.Identity.Name;

            // sprawdzenie czy istnieje taki rodzic
            if (directory.ParentId>=0)
            {
                try
                {
                    parent = _repository.FindBy(
                        ds => ds.Id == directory.ParentId 
                            && ds.File == null 
                            && ds.DeletedTime == null
                            //&& ds.CreatedBy.UserName == username
                            
                    ).First();

                    var b =_repository.FindBy(
                        ds => ds.Parent.Id == parent.Id 
                            && ds.Name == directory.Name 
                            && ds.DeletedTime == null)
                        .Any();
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

            var diskStructure = new DiskStructure
            {
                Name = directory.Name,
                Description = directory.Description,
                Parent = parent,
                CreatedBy = _repository.Context.Users.First(u => u.UserName == username)
            };

            _repository.Add(diskStructure);
            _repository.Save();

            return Ok(true);
        }

        // PUT: api/Directory/5
        /// <summary>
        /// Edytuje katalog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody]DirModels.Edit dir)
        {
            var username = User.Identity.Name;

            var b = _repository.FindBy(
                ds => ds.Parent.Id ==id 
                    && ds.Name == dir.Name 
                    && ds.DeletedTime == null
                    )
                        .Any();
            if (b)
            {
                ModelState["ParentId"].Errors.Add(Validation.ItemExistInDirectory);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!HaveAccessToDirectory(id, username)) return NotFound();

            var com = _repository
                .FindBy(
                    c => c.Id == id
                        && c.File == null
                        && c.DeletedTime == null
                    ).FirstOrDefault();

            if (com == null)
            {
                return NotFound();
            }

            com.Name = dir.Name;
            com.Description = dir.Description;
            com.ModifiedBy = _repository.Context.Users.First(u => u.UserName == username); 

            _repository.Edit(com);
            _repository.Save();

            return Ok(true);
        }

        // DELETE: api/Directory/5
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var username = User.Identity.Name;

            var entity = _repository
                .FindBy(
                    d => d.Id == id
                        && d.File == null
                        && d.DeletedTime == null
                        //&& d.CreatedBy.UserName == username
                    );

            if (entity == null && !entity.Any())
                return NotFound();

            var en = entity.FirstOrDefault();
            en.DeletedBy = _repository.Context.Users.First(u => u.UserName == username);
            _repository.Remove(en);

            _repository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET /api/directory/sharedforme
        [Route("SharedForMe")]
        public IEnumerable<DirModels.View> GetSharedForMe() {
            var username = User.Identity.Name;
            var tmp = DiskStructureHelper.FindShared(_repository, username, DiskItemType.Directory, PrivilageToDiskItemType.Shared);
            
            return ObjectConverter.ManyDirToApiView(tmp);
        }

        // GET /api/directory/sharedbyme
        [Route("SharedByMe")]
        public IEnumerable<DirModels.View> GetSheredByMe()
        {
            var username = User.Identity.Name;
            var tmp = DiskStructureHelper.FindShared(_repository, username, DiskItemType.Directory, PrivilageToDiskItemType.Owner);
            
            return ObjectConverter.ManyDirToApiView(tmp);
        }

        /// <summary>
        /// Zwraca pliki zawierające się w danym katalogu.
        /// </summary>
        /// <param name="id">Id katalogu, null - root</param>
        /// <returns></returns>
        /// 
        // GET /api/directory/5/files
        [HttpGet]
        [Route("{id:int}/files")]
        public IEnumerable<FileModels.View> GetFiles(int id)
        {
            var username = User.Identity.Name;

            var files = _repository.FindBy(
                ds =>
                    id == ds.Parent.Id
                    && ds.DeletedTime == null
                    && ds.File != null
                    /*&& (
                        ds.CreatedBy.UserName == username
                        || ds.AvailableFor.Select(a => a.User.UserName).Contains(username)
                    )*/
                );

            return ObjectConverter.ManyFilesToApiView(files);
        }

        // GET /api/directory/5/directories
        /// <summary>
        /// Zwraca katalogi-dzieci danego katalogu.
        /// </summary>
        /// <param name="id">id katalogu</param>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("{id:int}/directories")]
        public IEnumerable<DirModels.View> GetDirectories(int id)
        {
            var username = User.Identity.Name;

            var dirs = _repository.FindBy(
                    ds =>
                        id == ds.Parent.Id
                        && ds.DeletedTime == null
                        && ds.File == null
                        /*&& (
                            ds.CreatedBy.UserName == username
                            || ds.AvailableFor.Select(a => a.User.UserName).Contains(username)
                        )*/
                );

            return ObjectConverter.ManyDirToApiView(dirs);
        }

        // GET /api/directory/5/share
        [HttpGet]
        [Route("{id:int}/share/{username}")]
        public async Task<IHttpActionResult> ShareWith(int id, string username)
        {
            var usernameCur = User.Identity.Name;
            try
            {
                var dir = _repository.FindBy(
                        d => d.Id == id
                            && d.DeletedTime == null
                            && d.File == null
                    ).First();

                if (dir.CreatedBy.UserName == usernameCur)
                {

                    var tmp = DiskStructureHelper.ShareWith(_repository, dir, usernameCur);
                    _repository.Save();

                    return Ok(true);
                }

                return Ok(false);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
        }

        private bool HaveAccessToDirectory(int id, string username)
        {
            return  _repository.FindBy(
                     f => f.DeletedTime == null
                         && f.Id == id
                         && f.File == null
                         && (
                         f.CreatedBy.UserName == username
                         || f.AvailableFor.Select(a => a.User.UserName).Contains(username)
                         )
                 ).FirstOrDefault() != null;
        }
    }
}
