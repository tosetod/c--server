using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ServerInterfaces;

namespace ServerPlugins
{
    public static class GalleryFolderPlugin
    {
        internal static Response Generate(string[] imageFiles)
        {
            StringBuilder sb = new StringBuilder($@"
<ul>");
            int totalSizeInKb = 0;

            foreach (var image in imageFiles)
            {
                int imageSize = (int)new FileInfo(image).Length / 1000;
                sb.Append($"<li><img src=\"{image}\" /> size: {imageSize} kb</li>");
                totalSizeInKb += imageSize;
            };
            sb.Append("</ul>");
            sb.Append($@"<p>Total Images: {imageFiles.Length}</p>
<p>Total size of images: {totalSizeInKb} kb</p>");
            return new Response
            {
                ContentType = ContentTypes.HtmlText,
                Body = sb.ToString()
            };
        }
    }
}
