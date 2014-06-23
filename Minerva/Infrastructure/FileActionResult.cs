using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Minerva.Infrastructure
{
    public class FileActionResult : IHttpActionResult
    {
        private string _path;
        private string _filename;

        public FileActionResult(string path, string filename)
        {
            _path = path;
            _filename = filename;
        }

        public int FileId { get; private set; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            if (!File.Exists(_path))
            {
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            HttpResponseMessage response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            response.Content = new StreamContent(File.OpenRead(_path));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            if (!string.IsNullOrEmpty(_filename))
            {
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = _filename
                };
            }
            
            return Task.FromResult(response);
        }
    }
}