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

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania katalogami.
    /// </summary>
    //todo [Authorize]
    [RoutePrefix("api/Directory")]
    public class DirectoryController : ApiController
    {
        private GenericFullRepository<MinervaDbContext, DiskStructure> _repository;

        public DirectoryController()
        {
            _repository = new DiskStructureRepository();
        }

        // GET: api/Directory
        /// <summary>
        /// Zwraca listę wszystkich folderów
        /// </summary>
        /// <returns></returns>
        /// 
        public IEnumerable<DirModels.View> Get()
        {
            // todo ograniczenie do katalogu 
            
            return (
                from d in _repository.GetAll()
                where d.File == null
                select new DirModels.View
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Image = "", //todo
                } 
            );
        }

        // GET: api/Directory/5
        public async Task<IHttpActionResult> Get(int id)
        {
            // todo ograniczenie co do własności
            var dir = _repository
                .FindBy(
                    d => 
                        d.Id == id
                        && d.File == null
                ).FirstOrDefault();
            if (dir == null) return NotFound();

            return Ok(
                new DirModels.View
                {
                    Id = dir.Id,
                    Name = dir.Name,
                    Description = dir.Description,
                    Image = "" //todo
                }
           );
        }

        // POST: api/Directory
        /// <summary>
        /// Dodaje katalog
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post([FromBody]DirModels.Add directory)
        {
            // sprawdzenie czy istnieje taki rodzic
            DiskStructure parent = null;

            if (directory.ParentId>=0)
            {
                try
                {
                    var a = _repository.FindBy(
                        ds => ds.Id == directory.ParentId 
                            && ds.File == null 
                            && ds.DeletedTime == null
                    );

                    parent = a.First();

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
                CreatedBy = _repository.Context.Users.First(u => u.UserName == "Mariusz")
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
        public async Task<IHttpActionResult> Put(int id, [FromBody]DirModels.Edit dir)
        {
            var b = _repository.FindBy(ds => ds.Parent.Id ==id && ds.Name == dir.Name && ds.DeletedTime == null)
                        .Any();
            if (b)
            {
                ModelState["ParentId"].Errors.Add("Item with this name already exist in this direcotry");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
            com.ModifiedBy = _repository.Context.Users.First(u => u.UserName == "Mariusz"); 

            _repository.Edit(com);
            _repository.Save();

            return Ok(true);
        }

        // DELETE: api/Directory/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            // todo ograniczenie tylko do swoich

            var entity = _repository
                .FindBy(
                    d => d.Id == id
                        && d.File == null
                        && d.DeletedTime == null
                    );

            if (entity == null && !entity.Any())
                return NotFound();

            var en = entity.FirstOrDefault();
            en.DeletedBy = _repository.Context.Users.First(u => u.UserName == "Mariusz");
            _repository.Delete(en);
            _repository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("SharedForMe")]
        public IEnumerable<DirModels.View> GetSharedForMe() {
            return null;
        }

        [Route("SharedByMe")]
        public IEnumerable<DirModels.View> GetSheredByMe()
        {
            return null;
        }

        /// <summary>
        /// Zwraca pliki zawierające się w danym katalogu.
        /// </summary>
        /// <param name="id">Id katalogu, null - root</param>
        /// <returns></returns>
        /// 
        [Route("{id:int}/files")]
        public IEnumerable<FileModels.View> GetFiles(int id)
        {
            var files = _repository.FindBy(
                    ds =>
                        id == ds.Parent.Id
                        && ds.File != null
                );

            var retFiles = files.Select(
                f => new FileModels.View
                {
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description
                }
            );

            return retFiles;
        }

        [Route("{id:int}/directories")]
        /// <summary>
        /// Zwraca katalogi-dzieci danego katalogu.
        /// </summary>
        /// <param name="id">id katalogu</param>
        /// <returns></returns>
        public IEnumerable<DirModels.View> GetDirectories(int id)
        {
            var dirs = _repository.FindBy(
                    ds =>
                        id == ds.Parent.Id
                        && ds.File == null
                );

            var retDirs = dirs.Select(
                f => new DirModels.View
                {
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    Image = "" // todo
                }
            );

            return retDirs;
        }
    }
}
