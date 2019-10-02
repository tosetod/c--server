using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ServerInterfaces;

namespace ServerPlugins.SqlServer.CommandResponders
{
    public class GeneralInfo: ICommandResponder
    {
        private string ConnectionString { get; set; }

        public GeneralInfo(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public async Task<Response> GetResponse()
        {
            using (var cnn = new SqlConnection(ConnectionString))
            {
                cnn.Open();
                var command = new SqlCommand("select @@version", cnn);
                var result = (await command.ExecuteScalarAsync()).ToString();
                var versionDict = new Dictionary<string, string> { ["version"] = result };
                var json = JsonConvert.SerializeObject(versionDict);
                //var body = $@"{{""version"":""{result}""}}";

                return new Response
                {
                    ContentType = ContentTypes.JsonApplication,
                    ResponseCode = ResponseCode.Ok,
                    Type = ResponseType.Text,
                    Body = json
                };
            }
           
        }
    }
}
