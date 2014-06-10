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
using Minerva.Models.Api.Directory;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania katalogami.
    /// </summary>
    // todo dodać [Authorize]
    public class DirectoryController : ApiController
    {
        private GenericRepository<MinervaDbContext, Directory> _directoryRepository;
        private GenericFullRepository<MinervaDbContext, DiskStructure> _diskStructureRepository;

        public DirectoryController()
        {
            _directoryRepository = new DirectoryRepository();
            _diskStructureRepository = new DiskStructureRepository();
        }

        // GET: api/Directory
        /// <summary>
        /// Zwraca listę wszystkich folderów
        /// </summary>
        /// <returns></returns>
        public IEnumerable<View> Get()
        {
            // todo ograniczenie do katalogu 
            
            return (
                from d in _directoryRepository.GetAll()
                select new View {
                    Id = d.DiskStructureId,
                    Name = d.DiskStructure.Name,
                    Description = d.DiskStructure.Description,
                    Img = "", //todo
                } 
            );
        }

        // GET: api/Directory/5
        public async Task<IHttpActionResult> Get(int id)
        {
            // todo ograniczenie co do własności
            var dir = _directoryRepository.FindBy(d => d.DiskStructureId == id).FirstOrDefault();
            if (dir == null) return NotFound();

            return Ok(
                new View {
                    Id = dir.DiskStructureId,
                    Name = dir.DiskStructure.Name,
                    Description = dir.DiskStructure.Description,
                    Img = "" //todo
                }
           );
        }

        // POST: api/Directory
        public async Task<IHttpActionResult> Post([FromBody]Add directory)
        {
            // sprawdzenie czy istnieje taki rodzic
            DiskStructure parent = null;

            try
            {
                parent = _diskStructureRepository.FindBy(
                    ds => ds.Id == directory.ParentId 
                        && ds.Directory != null
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



            _directoryRepository.Save();

            return Ok();
        }

        // PUT: api/Directory/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Directory dir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = _directoryRepository.FindBy(c => c.DiskStructureId == dir.DiskStructureId).FirstOrDefault();

            if (com == null)
            {
                return NotFound();
            }

            _directoryRepository.Edit(dir);
            _directoryRepository.Save();

            return Ok();
        }

        // DELETE: api/Directory/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            // todo ograniczenie tylko do swoich
            var entity = _directoryRepository.FindBy(d => d.DiskStructureId == id).First();

            if (entity == null)
                return NotFound();

            _directoryRepository.Delete(entity);
            _directoryRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
