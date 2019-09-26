using ServerInterfaces;
using System;
using System.Threading.Tasks;

namespace ResponseGenerators
{
    public class FaviconResponseGenerator : IResponseGenerator
    {
        public int Count { get; }

        public async Task<Response> Generate(Request request, ILogger logger)
        {
            return new Response
            {
                ContentType = ContentTypes.IconImage
            };
        }

        public bool IsInterested(Request request, ILogger logger)
        {
            return request.Path.Equals("favicon.ico");
        }
    }
}
