using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Green_API_library
{
    class Program
    {
        static async Task Main(string[] args)
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            //Data
            string idInstance = "*****";
            string apiTokenInstance = "*****";
            string waNumber = "*****";

            //Send a test message
            HttpClient client = new HttpClient();
            WSP_API wsp_request = new WSP_API(idInstance, apiTokenInstance);
            await wsp_request.Send_WSP_msg(client, waNumber, "Test Message");
        }
    }
}
