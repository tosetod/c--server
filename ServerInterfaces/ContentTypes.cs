using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ServerInterfaces
{
    public class ContentTypes
    {
        #region Common Content Types
        private static Dictionary<string, string> CommonContentTypes = new Dictionary<string, string>
        {
            { ".jpg", JpegImage }, { ".jpeg", JpegImage }, { ".jpe", JpegImage },
            { ".png", "image/png" },
            { ".gif", "image/gif" },
            { ".htm", HtmlText }, { ".html", HtmlText },
            { ".txt", PlainText }, { ".js", PlainText }
        };
        #endregion

        public const string JpegImage = "image/jpeg";
        public const string JsonApplication = "application/json";
        public const string PlainText = "text/plain";
        public const string HtmlText = "text/html";
        public const string IconImage = "image/vnd.microsoft.icon";
        public const string Anything = "application/octet-stream";

        public static string GetContentType(string fullPath)
        {
            //var imageExts = new string[] { ".jpg", ".png", ".gif" };
            //var htmlExts = new string[] { ".htm", ".html" };
            //var isImage = imageExts.Contains(Path.GetExtension(fullPath));
            //var isHtml = htmlExts.Contains(Path.GetExtension(fullPath));

            //return isImage ? JpegImage : isHtml ? HtmlText : PlainText;

            var extension = Path.GetExtension(fullPath).ToLower();
            if (CommonContentTypes.ContainsKey(extension))
            {
                return CommonContentTypes[extension];
            }
            return Anything;
        }
    }

}

