using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Minerva.Models.Items;
using Minerva.Repositories;
using Minerva.Entities;
using Minerva.Entities.Sources.Internal;
using System.Threading.Tasks;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania informacjami dotyczącymi plików i katalogów.
    /// </summary>
    //todo dodać [Authorize]
    public class FileInfoController : ApiController
    {
        private GenericRepository<MinervaDbContext, File, Int64> _fileRepository;

        public FileInfoController()
        {
            _fileRepository = new FileInfoRepository();
        }

        // GET api/iteminfo
        public IEnumerable<File> Get()
        {
            // todo ograniczyć co do dostępności

            return _fileRepository.GetAll();
        }

        // GET api/iteminfo/5
        public File Get(int id)
        {
            // todo ograniczenie co do własności
            return _fileRepository.FindBy(f => f.Id == id).FirstOrDefault();
        }

        // POST api/iteminfo
        public async Task<IHttpActionResult> Post([FromBody]File file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _fileRepository.Add(file);
            _fileRepository.Save();

            return Ok();
        }

        // PUT api/iteminfo/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]File file)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = _fileRepository.FindBy(c => c.Id == file.Id).FirstOrDefault();

            if (com == null)
            {
                return NotFound();
            }

            _fileRepository.Edit(file);
            _fileRepository.Save();

            return Ok();
        }

        // DELETE api/iteminfo/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            // todo ograniczyć co do własności

            var entity = _fileRepository.FindBy(f => f.Id == id).FirstOrDefault();

            if (entity == null)
                return NotFound();

            _fileRepository.Delete(entity);
            _fileRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
