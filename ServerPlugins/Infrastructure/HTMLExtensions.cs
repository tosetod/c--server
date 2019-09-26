using ServerInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ServerPlugins.Infrastructure
{
    internal static class HTMLExtensions
    {
        public static Response AddSizeDiv(this Response response, string path)
        {
            
            var fileSize = new FileInfo(path).Length / 1000;
            response.Body.Concat($"<div>Total size of html file is {fileSize} kb</div>");
            
            return response;
        }
    }
}
