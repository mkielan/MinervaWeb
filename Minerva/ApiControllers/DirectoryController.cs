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

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania katalogami.
    /// </summary>
    // todo dodać [Authorize]
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
        public IEnumerable<DirModels.View> Get()
        {
            // todo ograniczenie do katalogu 
            
            return (
                from d in _repository.GetAll()
                where d.IsDirectory
                select new DirModels.View
                {
                    Id = d.Id,
                    Name = d.Name,
                    Description = d.Description,
                    Img = "", //todo
                } 
            );
        }

        // GET: api/Directory/5
        public async Task<IHttpActionResult> Get(long id)
        {
            // todo ograniczenie co do własności
            var dir = _repository
                .FindBy(
                    d => 
                        d.Id == id
                        && d.IsDirectory
                ).FirstOrDefault();
            if (dir == null) return NotFound();

            return Ok(
                new DirModels.View
                {
                    Id = dir.Id,
                    Name = dir.Name,
                    Description = dir.Description,
                    Img = "" //todo
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

            try
            {
                parent = _repository.FindBy(
                    ds => ds.Id == directory.ParentId 
                        && ds.IsDirectory
                        ).Single();
            }
            catch (ArgumentNullException exc)
            {
                ModelState["ParentId"].Errors.Add(exc);
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
            };

            var dir = new Directory
            {
                DiskStructure = diskStructure
            };


            _repository.Add(diskStructure);
            _repository.Save();

            return Ok();
        }

        // PUT: api/Directory/5
        /// <summary>
        /// Edytuje katalog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(long id, [FromBody]DirModels.Edit dir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = _repository
                .FindBy(
                    c => c.Id == id
                        && c.IsDirectory
                    ).FirstOrDefault();

            if (com == null)
            {
                return NotFound();
            }

            com.Name = dir.Name;
            com.Description = dir.Description;

            _repository.Edit(com);
            _repository.Save();

            return Ok();
        }

        // DELETE: api/Directory/5
        public async Task<IHttpActionResult> Delete(long id)
        {
            // todo ograniczenie tylko do swoich
            var entity = _repository
                .FindBy(
                    d => d.Id == id
                        && d.IsDirectory
                    ).First();

            if (entity == null)
                return NotFound();

            _repository.Delete(entity);
            _repository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Zwraca pliki zawierające się w danym katalogu.
        /// </summary>
        /// <param name="id">Id katalogu</param>
        /// <returns></returns>
        public IEnumerable<FileModels.View> GetFiles(long id)
        {
            var files = _repository.FindBy(
                    ds => ds.Parent.Id == id
                        && ds.IsFile == true
                );

            var retFiles = files.Select(
                f => new FileModels.View { 
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description
                }
            );

            return retFiles;
        }

        /// <summary>
        /// Zwraca katalogi-dzieci danego katalogu.
        /// </summary>
        /// <param name="id">id katalogu</param>
        /// <returns></returns>
        public IEnumerable<DirModels.View> GetDirectories(long id)
        {
            var dirs = _repository.FindBy(
                    ds => ds.Parent.Id == id
                        && ds.IsDirectory == true
                );

            var retDirs = dirs.Select(
                f => new DirModels.View
                {
                    Id = f.Id,
                    Name = f.Name,
                    Description = f.Description,
                    Img = "" // todo
                }
            );

            return retDirs;
        }
    }
}
