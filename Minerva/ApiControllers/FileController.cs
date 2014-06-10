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
using Minerva.Models.Api.File;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania plikami.
    /// </summary>
    //todo dodać [Authorize]
    public class FileController : ApiController
    {
        private GenericRepository<MinervaDbContext, DiskStructure> _repository;
        private GenericRepository<MinervaDbContext, Tag> _tagRepository;

        public FileController()
        {
            //string path = ConfigurationSettings.AppSettings["FilesStoragePath"];
            _repository = new DiskStructureRepository();
            _tagRepository = new TagRepository();
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            // todo ograniczenie dla danego usera
            var file = _repository
                .FindBy(f => f.Id == id && f.IsFile)
                .FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            return Ok(new View {
                Id = file.Id,
                Name = file.Name,
                Tags = file.Tags.Select(t => t.Name).ToArray(),
                Url = "url", //todo
                Description = file.Description,
                Ext = file.File.Extension,
                Phone = file.CreatedBy != null && file.CreatedBy.Phone != null ?file.CreatedBy.Phone : "",
                Creator = file.CreatedBy != null ? file.CreatedBy.UserName : "",
                LastModificator = file.ModifiedBy != null ? file.ModifiedBy.UserName : ""
            });
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody]Add file)
        {// todo ograniczenie dla danego usera
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ds = new DiskStructure
            {
                Name = file.Name,
                Description = file.Description
            };

            _repository.Add(ds);
            _repository.Save();

            return Ok();
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Edit file)
        {// todo ograniczenie dla danego usera
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var com = _repository.FindBy(c => c.Id == id).FirstOrDefault();

            if (com == null)
            {
                return NotFound();
            }

            com.Name = file.Name;
            com.Description = file.Description;

            if(file.Tags != null) {
                foreach(var tag in file.Tags) 
                {
                    var tagEntity = _repository.Context.Tags.FirstOrDefault(t => t.Name == tag);

                    if (tagEntity == null)
                    {
                        tagEntity = new Tag { Name = tag };
                        _tagRepository.Add(tagEntity);
                        _tagRepository.Save();
                    }

                    com.Tags.Add(tagEntity);
                }
            }
            else
            {
                com.Tags.Clear();
            }

            _repository.Edit(com);
            _repository.Save();

            return Ok();
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            // todo ograniczenie co do własności
            var entity = _repository
                .FindBy(f => f.Id == id && f.IsFile)
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            _repository.Delete(entity);
            _repository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        public IEnumerable<View> FindByTag(string id)
        {
            return null; //todo
        }
    }
}