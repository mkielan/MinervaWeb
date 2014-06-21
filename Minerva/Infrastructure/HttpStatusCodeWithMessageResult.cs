using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Minerva.Infrastructure
{
    public class HttpStatusCodeWithMessageResult : IHttpActionResult
    {
        private string _message;
        private HttpStatusCode _code;

        public HttpStatusCodeWithMessageResult(HttpStatusCode code, string message = null)
        {
            _message = message;
            _code = code;
        }
    
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(_code);

            if(!string.IsNullOrEmpty(_message)) response.Content = new StringContent(_message);

            return Task.FromResult(response);
        }
    }
}