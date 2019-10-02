using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using ServerInterfaces;

namespace ServerCore.Requests
{
    internal static class RequestProcessor
    {
        public static Request ProcessRequest(Socket socket, ILogger logger)
        {
            //byte[] file;
            byte[] buffer = new byte[128*1024];

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    int read = socket.Available;
            //    while ((read != 0))
            //    {

            //        ms.Write(buffer, 0, read);
            //    }

            //    file = ms.ToArray();
            //}

            int receivedCount = socket.Receive(buffer);
            logger.Debug($"Received {receivedCount} bytes from socket");
            var readString = Encoding.ASCII.GetString(buffer, 0, receivedCount);

            string fileString = Convert.ToBase64String(buffer);



            byte[] fileBytes = Convert.FromBase64String(fileString);

            logger.Debug(readString);
            if (string.IsNullOrEmpty(readString))
            {
                logger.Info("Detected empty request");
                return Request.EmptyRequest;
            }

            var parser = new RequestParser();
            var result = parser.Parse(readString);
            return result;
        }
    }
}
