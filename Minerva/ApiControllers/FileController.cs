using Minerva.Entities;
using Minerva.Entities.Sources.Internal;
using Minerva.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania plikami.
    /// </summary>
    public class FileController : ApiController
    {
        private GenericRepository<MinervaDbContext, File, Int64> _fileRepository;

        public FileController()
        {
            _fileRepository = new FileRepository(""); // todo path
        }

        // GET api/<controller>/5
        public File Get(int id)
        {
            // todo ograniczenie dla danego usera
            return _fileRepository.FindBy(f => f.Id == id).First();
        }

        // POST api/<controller>
        public void Post([FromBody]File value)
        {
            throw new NotImplementedException();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]File value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            // todo ograniczenie co do własności
            var entity = _fileRepository.FindBy(f => f.Id == id).First();

            _fileRepository.Delete(entity);
            _fileRepository.Save();
        }
    }
}