using ServerInterfaces;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ResponseGenerators
{
    public class FaviconResponseGenerator : IResponseGenerator
    {
        public int Count { get; }

        public async Task<Response> Generate(Request request, ILogger logger)
        {
            var bytes = await File.ReadAllBytesAsync(@"C:\Users\Tosho.Todorovski\Desktop\Projects\csharp-server\ServerRunner\favicon.ico");

            logger.Debug($"Read image {request.Path} from disk");
            return new Response
            {
                Bytes = bytes,
                Type = ResponseType.Binary,
                ContentType = ContentTypes.IconImage
            };
        }

        public bool IsInterested(Request request, ILogger logger)
        {
            return request.Path.Equals("favicon.ico");
        }
    }
}
