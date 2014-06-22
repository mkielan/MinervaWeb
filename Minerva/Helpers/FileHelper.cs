using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Minerva.Helpers
{
    public static class FileHelper
    {
        public static bool Save(HttpRequestBase httpRequest, string destination) {
            if (httpRequest.Files.Count == 1)
            {
                var postedFile = httpRequest.Files[0];
                postedFile.SaveAs(destination);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}