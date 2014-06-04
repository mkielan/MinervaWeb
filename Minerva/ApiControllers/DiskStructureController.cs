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
    public class DiskStructureController : ApiController
    {
        private GenericRepository<MinervaDbContext, DiskStructure, Int64> _diskStrRepository;

        public DiskStructureController(
            GenericRepository<MinervaDbContext, DiskStructure, Int64> diskStrRepository)
        {
            _diskStrRepository = diskStrRepository;
        }

        // GET: api/DiskStructure
        public IEnumerable<DiskStructure> Get()
        {
            //todo ograniczenie co do własności

            return _diskStrRepository.GetAll();
        }

        // GET: api/DiskStructure/5
        public DiskStructure Get(int id)
        {
            //todo ograniczenie co do własności

            return _diskStrRepository.FindBy(f => f.Id == id).First();
        }

        // POST: api/DiskStructure
        public void Post([FromBody]DiskStructure value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/DiskStructure/5
        public void Put(int id, [FromBody]DiskStructure value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/DiskStructure/5
        public void Delete(int id)
        {
            //todo ograniczenie co do własności

            var entity = _diskStrRepository.FindBy(f => f.Id == id).First();

            _diskStrRepository.Delete(entity);
            _diskStrRepository.Save();
        }
    }
}
