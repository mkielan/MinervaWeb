using Minerva.Entities;
using Minerva.Entities.Sources.Internal;
using Minerva.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Minerva.ApiControllers
{
    public class DiskStructureController : ApiController
    {
        private GenericRepository<MinervaDbContext, DiskStructure, Int64> _diskStrRepository;

        public DiskStructureController()
        {
            _diskStrRepository = new DiskStructureRepository();
        }

        // GET: api/DiskStructure
        public IEnumerable<DiskStructure> Get()
        {
            //todo ograniczenie co do własności

            return _diskStrRepository.GetAll();
        }

        // GET: api/DiskStructure/5
        public async Task<IHttpActionResult> Get(int id)
        {
            //todo ograniczenie co do własności
            var ds = _diskStrRepository.FindBy(f => f.Id == id).FirstOrDefault();

            if (ds == null) return NotFound();

            return Ok(ds);
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
