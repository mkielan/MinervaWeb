using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Minerva.Infrastructure
{
    public class FileActionResult : IHttpActionResult
    {
        private string _path;

        public FileActionResult(string path)
        {
            _path = path;
        }

        public int FileId { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (!File.Exists(_path))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response.Content = new StreamContent(File.OpenRead(_path));

            return Task.FromResult(response);
        }
    }
}