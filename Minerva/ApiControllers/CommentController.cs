using Minerva.Entities;
using Minerva.Entities.Sources;
using Minerva.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Minerva.ApiControllers
{
    public class CommentController : ApiController
    {
        private GenericRepository<MinervaDbContext, Comment, Int64> _commentRepository;

        public CommentController(
            GenericRepository<MinervaDbContext, Comment, Int64> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // GET: api/Comment
        public IEnumerable<Comment> Get()
        {
            // todo ogranczenie co do własności
            return _commentRepository.GetAll();
        }

        // GET: api/Comment/5
        public Comment Get(int id)
        {
            // todo ogranczenie co do własności
            return _commentRepository.FindBy(f => f.Id == id).First();
        }

        // POST: api/Comment
        public void Post([FromBody]Comment value)
        {
            throw new NotImplementedException();
        }

        // PUT: api/Comment/5
        public void Put(int id, [FromBody]Comment value)
        {
            throw new NotImplementedException();
        }

        // DELETE: api/Comment/5
        public void Delete(int id)
        {
            // todo ogranczenie co do własności

            var entity = _commentRepository.FindBy(f => f.Id == id).First();

            _commentRepository.Delete(entity);
            _commentRepository.Save();
        }
    }
}
