using Minerva.Entities;
using Minerva.Entities.Sources;
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
    //todo dodać [Authorize]
    public class CommentController : ApiController
    {
        private GenericRepository<MinervaDbContext, Comment, Int64> _commentRepository;

        public CommentController()
        {
            _commentRepository = new CommentRepository();
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
        public async Task<IHttpActionResult> Post([FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _commentRepository.Add(comment);
            _commentRepository.Save();

            return Ok();
        }

        // PUT: api/Comment/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = _commentRepository.FindBy(c => c.Id == comment.Id).FirstOrDefault();
            
            if (com == null)
            {
                return NotFound();
            }

            _commentRepository.Edit(comment);
            _commentRepository.Save();

            return Ok();
        }

        // DELETE: api/Comment/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            // todo ogranczenie co do własności

            var entity = _commentRepository.FindBy(f => f.Id == id).FirstOrDefault();

            if (entity == null)
                return NotFound();

            _commentRepository.Delete(entity);
            _commentRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}
