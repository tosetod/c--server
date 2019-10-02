using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;

namespace ServerPlugins
{
    public class InternalServerErrorPostProcessor : IResponsePostProcessor
    {
        public bool IsInterested(Response response, ILogger logger)
        {
            return response.ResponseCode == ResponseCode.InternalServerError;
        }

        public Task<Response> Process(Response response, ILogger logger)
        {
            response.Type = ResponseType.Text;
            response.ContentType = ContentTypes.HtmlText;
            response.Body = "<h4>The server can't process your request right now. Please try again later...</h4>";
            return Task.FromResult(response);
        }
    }
}
