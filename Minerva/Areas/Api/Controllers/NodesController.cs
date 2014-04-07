using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Minevra.Areas.Api.Controllers
{
    public class NodesController : ApiController
    {
        public NodesController()
        {

        }

        public JsonResult GetChildrenInfo(int? parentId)
        {
            throw new NotImplementedException();
        }

        public JsonResult GetChild(int? itemId)
        {
            throw new NotImplementedException();
        }

        public ActionResult GetFile(int fileId)
        {
            throw new NotImplementedException();
        }
    }
}
