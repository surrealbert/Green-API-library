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
            string idInstance = "*****"; //Write your instance ID
            string apiTokenInstance = "*****"; //Write your instance token
            string waNumber = "*****"; //Write whatsapp number

            //Send a test message
            //1) Create an HTTP client object
            //2) Create a WSP_API object

            HttpClient HTTPclient = new HttpClient();
            WSP_API TestRequest = new WSP_API(idInstance, apiTokenInstance);
            await TestRequest.SendWspMessage(HTTPclient, waNumber, "Test Message");
        }
    }
}
