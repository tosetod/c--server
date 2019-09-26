using ServerInterfaces;
using ServerPlugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponseGenerators
{
    public class IndexResponseGenerator : StaticResponseGenerator
    {
        public IndexResponseGenerator(string folderName) : base(folderName)
        {
        }

        public override async Task<Response> Generate(Request request, ILogger logger)
        {
            var indexPath = Path.Combine(FolderPath, "index.html");
            var indexHtml = await File.ReadAllTextAsync(indexPath);
            return new Response
            {
                ContentType = ContentTypes.HtmlText,
                Body = indexHtml

            };
        }

        public override bool IsInterested(Request request, ILogger logger)
        {
            return string.IsNullOrEmpty(request.Path);
        }
    }
}
