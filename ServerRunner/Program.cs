using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using PngResponseGeneratorLib;
using ResponseGenerators;
using ServerCore;
using ServerInterfaces;
using ServerPlugins;

namespace ServerRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new WebServer();
            server
                .UseResponseGenerator<PngResponseGenerator>()
                .UseResponseGenerator<PostMethodResponseGenerator>()
                .UseResponseGenerator(new StaticResponseGenerator(@"C:\Users\Tosho.Todorovski\Desktop\Projects\csharp-server\SedcServer\ServerRunner\static"))
                .UseResponseGenerator<FaviconResponseGenerator>()
                .UseResponsePostProcessor<NotFoundPostProcessor>();


            //var result = server.Run(@"C:\Users\Toshe\Desktop\Projects\csharp-server\ServerCore\cert.cer");
            var result = server.Run();
            result.Wait();
        }
    }
}
