using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerInterfaces;
using ServerPlugins.Infrastructure;

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
            var path = string.Join(Path.DirectorySeparatorChar, request.Path.Split("/").Skip(2));
            var fullPath = Path.Combine(FolderPath, path);
            if (!File.Exists(fullPath))
            {
                var indexPath = Path.Combine(fullPath, "index.html");
                if (Directory.Exists(fullPath))
                {
                    var imageFiles = Directory.GetFiles(fullPath, "*.jpg");
                    if (imageFiles.Length > 1)
                    {
                        return GalleryFolderPlugin.Generate(imageFiles);
                        
                    }
                }
                if (!File.Exists(indexPath))
                {
                    return new NotFoundResponse();
                }

                return await GenerateHtml(indexPath);
            }

            
            var contentType = ContentTypes.GetContentType(fullPath);
            if (contentType == ContentTypes.HtmlText)
            {
                return await GenerateHtml(fullPath);
            }

            var bytes = await File.ReadAllBytesAsync(fullPath);
            var response = new Response
            {
                Bytes = bytes,
                Type = ResponseType.Binary,
                ContentType = contentType
            };
           
            return response;
        }

        public static async Task<Response> GenerateHtml(string fullPath)
        {
            var fileSize = new FileInfo(fullPath).Length;
            string html = await File.ReadAllTextAsync(fullPath);
            StringBuilder sb = new StringBuilder(html);
            sb.Append($"<div>Total size of html file is {fileSize} bytes</div>");
            return new Response
            {
                ContentType = ContentTypes.HtmlText,
                Body = sb.ToString()

            };
        }

        public virtual bool IsInterested(Request request, ILogger logger)
        {
            var path = $"{FolderName}";
            return request.Path.StartsWith(path, StringComparison.InvariantCultureIgnoreCase)
                || string.IsNullOrEmpty(request.Path);
        }
    }
}
