using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Minerva.Models.ItemInfo;

namespace Minerva.ApiControllers
{
    public class ItemInfoController : ApiController
    {
        // GET api/iteminfo
        public IEnumerable<Item> Get()
        {
            return null;
        }

        // GET api/iteminfo/5
        public Item Get(int id)
        {
            return null;
        }

        // POST api/iteminfo
        public void Post([FromBody]Item value)
        {
        }

        // PUT api/iteminfo/5
        public void Put(int id, [FromBody]Item value)
        {
        }

        // DELETE api/iteminfo/5
        public void Delete(int id)
        {
        }
    }
}
