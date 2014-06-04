using Minerva.Entities;
using Minerva.Repositories;
using Minerva.Entities.Sources.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania katalogami.
    /// </summary>
    public class DirectoryController : ApiController
    {
        private GenericRepository<MinervaDbContext, Directory, Int64> _directoryRepository;

        public DirectoryController(GenericRepository<MinervaDbContext, Directory, Int64> directoryRepository)
        {
            _directoryRepository = directoryRepository;
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
            return _directoryRepository.FindBy(d => d.Id == id).First();
        }

        // POST: api/Directory
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Directory/5
        public void Put(int id, [FromBody]string value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Directory/5
        public void Delete(int id)
        {
            // todo ograniczenie tylko do swoich
            var entity = _directoryRepository.FindBy(d => d.Id == id).First();

            _directoryRepository.Delete(entity);
            _directoryRepository.Save();
        }
    }
}
