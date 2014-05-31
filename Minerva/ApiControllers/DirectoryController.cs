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
        // GET: api/Directory
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Directory/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Directory
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Directory/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Directory/5
        public void Delete(int id)
        {
        }
    }
}
