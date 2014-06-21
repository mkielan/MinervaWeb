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
    [RoutePrefix("api/file")]
    public class FileController : ApiController
    {
        private GenericRepository<MinervaDbContext, DiskStructure> _repository;
        private GenericRepository<MinervaDbContext, Tag> _tagRepository;

        public FileController()
        {
            //string path = ConfigurationSettings.AppSettings["FilesStoragePath"];
            var context = new MinervaDbContext();
            _repository = new DiskStructureRepository(context);
            _tagRepository = new TagRepository(context);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            // todo ograniczenie dla danego usera
            var file = _repository
                .FindBy(f => f.Id == id && f.File != null)
                .FirstOrDefault();

            if (file == null)
            {
                return NotFound();
            }

            return Ok(new View {
                Id = file.Id,
                Name = file.Name,
                Tags = file.Tags != null ? file.Tags.Select(t => t.Name).ToArray() : null,
                Url = "url", //todo
                Description = file.Description,
                Extension = file.File.Extension ?? "",
                Phone = file.CreatedBy != null && file.CreatedBy.Phone != null ?file.CreatedBy.Phone : "",
                Creator = file.CreatedBy != null ? file.CreatedBy.UserName : "",
                LastModificator = file.ModifiedBy != null ? file.ModifiedBy.UserName : ""
            });
        }

        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody]Add file)
        {// todo ograniczenie dla danego usera
            DiskStructure parent = null;

            if (file.ParentId >= 0)
            {
                try
                {
                    var a = _repository.FindBy(
                        ds => ds.Id == file.ParentId 
                            && ds.File == null 
                            && ds.DeletedTime == null
                    );

                    parent = a.First();

                    var b = _repository.FindBy(ds => ds.Parent.Id == parent.Id && ds.Name == file.Name && ds.DeletedTime == null)
                        .Any();
                    if (b)
                    {
                        ModelState["ParentId"].Errors.Add("Item with this name already exist in this direcotry");
                    }
                }
                catch (ArgumentNullException exc)
                {
                    ModelState["ParentId"].Errors.Add(exc);
                }
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = _repository.Context.Users.First(u => u.UserName == "Mariusz");
            var diskStructure = new DiskStructure
            {
                Name = file.Name,
                Description = file.Description,
                Parent = parent,
                CreatedBy = user,
                CreatedTime = DateTime.Now
            };

            var tmp = diskStructure.Name.Split('.');

            _repository.Add(diskStructure);
            var f = new File { DiskStructureId = 
                diskStructure.Id, 
                DiskStructure = diskStructure,
                Extension = tmp.Length > 1 ? tmp.Last() : null
            };
            _repository.Context.Files.Add(f);

            _repository.Save();

            return Ok(true);
        }

        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Edit file)
        {// todo ograniczenie dla danego usera

            var b = _repository.FindBy(ds => ds.Parent.Id == id && ds.Name == file.Name && ds.DeletedTime == null)
                        .Any();
            if (b)
            {
                ModelState["ParentId"].Errors.Add("Item with this name already exist in this direcotry");
            }

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
            com.ModifiedBy = _repository.Context.Users.First(u => u.UserName == "Mariusz");
            var user = _repository.Context.Users.First(u => u.UserName == "Mariusz");
            if(file.Tags != null) {

                if (com.Tags == null) com.Tags = new List<Tag>();

                foreach(var tag in file.Tags) 
                {
                    var tagEntity = _repository.Context.Tags.FirstOrDefault(t => t.Name == tag);

                    if (tagEntity == null)
                    {
                        tagEntity = new Tag { Name = tag };
                        tagEntity.CreatedTime = DateTime.Now;
                        tagEntity.CreatedBy = user;
                        _repository.Context.Tags.Add(tagEntity);
                    }
                    com.Tags.Add(tagEntity);
                }
            }
            else
            {
                if(com.Tags != null) com.Tags.Clear();
            }

            _repository.Edit(com);
            _repository.Save();

            return Ok(true);
        }

        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(int id)
        {
            // todo ograniczenie co do własności
            var entity = _repository
                .FindBy(f => f.Id == id && f.File != null && f.DeletedTime == null)
                .FirstOrDefault();

            if (entity == null)
                return NotFound();

            entity.DeletedBy = _repository.Context.Users.First(u => u.UserName == "Mariusz");
            
            _repository.Remove(entity);
            _repository.Save();

            return StatusCode(HttpStatusCode.NoContent);
        }

        //[Route("{tag}/FindByTag")]
        //[ActionName("FindByTag")]
        [HttpGet]
        public IEnumerable<View> FindByTag(string id)
        {
            var _tag = _repository.Context.Tags.Include("DiskStructures").Where(t => t.Name == id).FirstOrDefault();

            if(_tag != null) {
                return (
                    from t in _tag.DiskStructures 
                    where t.File != null
                        && t.DeletedTime == null
                    select new View
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Tags = t.Tags != null ? t.Tags.Select(tt => tt.Name).ToArray() : null,
                        Url = "url", //todo
                        Description = t.Description,
                        Extension = t.File.Extension ?? "",
                        Phone = t.CreatedBy != null && t.CreatedBy.Phone != null ? t.CreatedBy.Phone : "",
                        Creator = t.CreatedBy != null ? t.CreatedBy.UserName : "",
                        LastModificator = t.ModifiedBy != null ? t.ModifiedBy.UserName : ""
                    }
                            );
            }
            
            return null; //todo
        }
    }
}