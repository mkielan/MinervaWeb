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
using MComment = Minerva.Models.Api.Comment;

namespace Minerva.ApiControllers
{
    //todo dodać [Authorize]
    public class CommentController : ApiController
    {
        private GenericFullRepository<MinervaDbContext, Comment> _commentRepository;

        public CommentController()
        {
            _commentRepository = new CommentRepository(new MinervaDbContext());
        }

        // GET: api/Comment
        public IEnumerable<Comment> Get()
        {
            // todo ogranczenie co do własności
            return _commentRepository.GetAll();
        }

        // GET: api/Comment/5
        public async Task<IHttpActionResult> Get(int id)
        {
            // todo ogranczenie co do własności
            var comment = _commentRepository.FindBy(f => f.Id == id).FirstOrDefault();
            if (comment == null)
            {
                return NotFound();
            }

            return Ok();
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

            _commentRepository.Remove(entity);
            _commentRepository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Get items for diskstructure.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<MComment.View> GetItemComments(long id)
        {
            return _commentRepository
                .FindBy(c => c.DiskStructure.Id == id)
                .Select(
                    c => new MComment.View { 
                        Id = c.Id,
                        Body = c.Body,
                        Username = c.CreatedBy.UserName,
                        Sended = c.CreatedTime
                    }
                )
                .OrderBy(c => c.Sended);
        }
    }
}
