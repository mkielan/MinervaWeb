using Minerva.Entities;
using Minerva.Repositories;
using Minerva.Entities.Sources.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania katalogami.
    /// </summary>
    // todo dodać [Authorize]
    public class DirectoryController : ApiController
    {
        private GenericRepository<MinervaDbContext, Directory, Int64> _directoryRepository;

        public DirectoryController()
        {
            _directoryRepository = new DirectoryRepository();
        }

        // GET: api/Directory
        public IEnumerable<Directory> Get()
        {
            // todo ograniczenie do katalogu 
            return _directoryRepository.GetAll();
        }

        // GET: api/Directory/5
        public Directory Get(int id)
        {
            // todo ograniczenie co do własności
            return _directoryRepository.FindBy(d => d.Id == id).FirstOrDefault();
        }

        // POST: api/Directory
        public async Task<IHttpActionResult> Post([FromBody]Directory directory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _directoryRepository.Add(directory);
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

            var com = _directoryRepository.FindBy(c => c.Id == dir.Id).FirstOrDefault();

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
            var entity = _directoryRepository.FindBy(d => d.Id == id).First();

            if (entity == null)
                return NotFound();

            _directoryRepository.Delete(entity);
            _directoryRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
