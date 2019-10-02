using ServerInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore.Responses
{
    public class FileUploadGenerator : IResponseGenerator
    {
        public int Count { get; }

        public async Task<Response> Generate(Request request, ILogger logger)
        {
            
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(request.Body));
            //var writer = new StreamWriter(stream);
            //writer.Write(request.Body);
            //writer.Flush();
            //stream.Position = 0;
            using (FileStream file = new FileStream(@"C:\Users\Tosho.Todorovski\Desktop\Projects\csharp-server\ServerRunner\static\test.jpeg", FileMode.Create, System.IO.FileAccess.Write)) {
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                file.Write(bytes, 0, bytes.Length);
                stream.Close();
            }
            return  new Response {Body = "successfully uploaded" };
        }

        public bool IsInterested(Request request, ILogger logger)
        {

            return request.Method == Method.Post;// && request.Headers.GetHeader("Content-Type") == "x-www-form-urlencoded";
        }
    }
}
