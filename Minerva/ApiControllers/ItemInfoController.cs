using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Minerva.Models.Items;

namespace Minerva.ApiControllers
{
    /// <summary>
    /// Api do zarządzania informacjami dotyczącymi plików i katalogów.
    /// </summary>
    public class ItemInfoController : ApiController
    {
        // GET api/iteminfo
        public IEnumerable<Info> Get()
        {
            return null;
        }

        // GET api/iteminfo/5
        public Info Get(int id)
        {
            return null;
        }

        // POST api/iteminfo
        public void Post([FromBody]Info value)
        {
        }

        // PUT api/iteminfo/5
        public void Put(int id, [FromBody]Info value)
        {
        }

        // DELETE api/iteminfo/5
        public void Delete(int id)
        {
        }
    }
}
