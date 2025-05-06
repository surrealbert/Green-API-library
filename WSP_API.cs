using System;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

public class WSP_API
{
    string IdInstance { get; set; }
    string apiTokenInstance { get; set; }

    public WSP_API(string IdInstance, string apiTokenInstance)
    {
        this.IdInstance = IdInstance;
        this.apiTokenInstance = apiTokenInstance;
    }

    // Clase que define el formato JSON de los comandos para utilizar el API de Whatsapp
    public class Wsp_JSON
    {
        public int statusCode { get; set; }
        public DateTime timestamp { get; set; }
        public string path { get; set; }
        public string message { get; set; }
    
    }

    public async Task Send_WSP_msg(HttpClient client, string numero, string mensaje)
    {
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.greenapi.com/waInstance" + this.IdInstance + "/sendMessage/" + this.apiTokenInstance);
        //request.Headers.Add("Authorization", "Bearer " + apiTokenInstance);

        request.Content = new StringContent("{" +
            "\"chatId\": \"" + numero + "@c.us\"," +
            "\"message\": \"" + mensaje + "\"" +
            "}");

        string message_response = await WSP_API_request(client, request);
        Console.WriteLine(message_response);
        Wsp_JSON online_status = JsonConvert.DeserializeObject<Wsp_JSON>(message_response.ToString());
    }

    public async Task<string> Upload_WSP_file(HttpClient client, string filepath, string file_type)
    {
        //file_type = "image/jpeg" para imagenes y "application/pdf" para PDF
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.green-api.com/waInstance" + this.IdInstance + "/uploadFile/" + this.apiTokenInstance);
        byte[] fileContent = File.ReadAllBytes(filepath);
        request.Content = new ByteArrayContent(fileContent);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue(file_type);
        string message_response = await WSP_API_request(client, request);

        return message_response;
    }

    public async Task Send_WSP_file(HttpClient client, string numero, string image_url, string file_name, string file_format)
    {
        //Construye el HTTP request con el enlace y los headers tal como el comando CURL
        HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://api.green-api.com/waInstance" + this.IdInstance + "/sendFileByUrl/" + this.apiTokenInstance);
        request.Content = new StringContent("{" +
            "\"chatId\": \"" + numero + "@c.us\"," +
            "\"urlFile\": \"" + image_url + "\"," +
            "\"fileName\": \"" + file_name + "." + file_format + "\"" +
            "}");

        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        string message_response = await WSP_API_request(client, request);
        Wsp_JSON online_status = JsonConvert.DeserializeObject<Wsp_JSON>(message_response.ToString());
    }

    public async Task<string> WSP_API_request(HttpClient client, HttpRequestMessage request)
    {
        string responseBody = "";
        try
        {
            HttpResponseMessage response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            responseBody = await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException error)
        {
            Console.WriteLine("\nException Caught!");
            Console.WriteLine("Message :{0} ", error.InnerException.Message);
        }

        return responseBody;
    }

    public string URL_replace(string url_response)
    {
        string mssg = url_response.Replace("{\"urlFile\":\"", "").Replace("\"}", "");

        return mssg;
    }

}

