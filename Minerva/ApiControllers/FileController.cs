using Minerva.Entities;
using Minerva.Entities.Sources;
using Minerva.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania plikami.
    /// </summary>
    //todo dodać [Authorize]
    public class FileController : ApiController
    {
        private GenericRepository<MinervaDbContext, File, Int64> _fileRepository;

        public FileController()
        {
            string path = ConfigurationSettings.AppSettings["FilesStoragePath"];
            _fileRepository = new FileRepository(HttpContext.Current.Server.MapPath(path));
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            // todo ograniczenie dla danego usera
            var file = _fileRepository.FindBy(f => f.Id == id).FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            return Ok(file);
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody]File file)
        {// todo ograniczenie dla danego usera
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _fileRepository.Add(file);
            _fileRepository.Save();

            return Ok();
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]File file)
        {// todo ograniczenie dla danego usera
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

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            // todo ograniczenie co do własności
            var entity = _fileRepository.FindBy(f => f.Id == id).FirstOrDefault();

            if (entity == null)
                return NotFound();

            _fileRepository.Delete(entity);
            _fileRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}