using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Minerva.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        // POST: File\Upload
        [HttpPost]
        public async Task<JsonResult> Upload(HttpPostedFileBase file)
        {
            return Json(true);
        }
    }
}