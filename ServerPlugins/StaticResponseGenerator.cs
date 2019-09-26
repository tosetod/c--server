using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ServerInterfaces;

namespace ServerPlugins
{
    public class StaticResponseGenerator : IResponseGenerator
    {
        public int Count { get; }
        public string FolderName { get; private set; }
        public string FolderPath { get; private set; }

        public StaticResponseGenerator (string folderName)
        {
            FolderName = Path.GetFileName(folderName);
            FolderPath = Path.GetFullPath(folderName);
        }

        public virtual async Task<Response> Generate(Request request, ILogger logger)
        {
            var path = string.Join(Path.DirectorySeparatorChar, request.Path.Split("/").Skip(1));
            if (string.IsNullOrEmpty(path.Trim()))
            {
                var indexPath = Path.Combine(FolderPath, "index.html");
                var indexHtml = await File.ReadAllTextAsync(indexPath);
                return new Response
                {
                    ContentType = ContentTypes.HtmlText,
                    Body = indexHtml

                };
            }
            var fullPath = Path.Combine(FolderPath, path);
            if (!File.Exists(fullPath))
            {
               return new NotFoundResponse();
            }

            var bytes = await File.ReadAllBytesAsync(fullPath);

            return new Response
            {
                Bytes = bytes,
                Type = ResponseType.Binary,
                ContentType = ContentTypes.GetContentType(fullPath)
            };
        }

        public virtual bool IsInterested(Request request, ILogger logger)
        {
            var path = $"{FolderName}";
            return request.Path.StartsWith(path, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
