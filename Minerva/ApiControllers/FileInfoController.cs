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

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania informacjami dotyczącymi plików i katalogów.
    /// </summary>
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
            return _fileRepository.FindBy(f => f.Id == id).First();
        }

        // POST api/iteminfo
        public void Post([FromBody]File value)
        {
            throw new NotImplementedException();
        }

        // PUT api/iteminfo/5
        public void Put(int id, [FromBody]File value)
        {
            throw new NotImplementedException();
        }

        // DELETE api/iteminfo/5
        public void Delete(int id)
        {
            // todo ograniczyć co do własności

            var entity = _fileRepository.FindBy(f => f.Id == id).First();
            _fileRepository.Delete(entity);
            _fileRepository.Save();
        }
    }
}
