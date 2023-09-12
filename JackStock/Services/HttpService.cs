using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace JackStock.Services
{
    internal class HttpService
    {
        protected string _url;
        protected HttpClient _client;

        public HttpService(string uri)
        {
            _url = uri;
            _client = new HttpClient();
        }

        public async Task GetAnsyc(string route, string param)
        {
            try
            {
                var apiUrl = $"{_url}/{route}?{param}";
                HttpResponseMessage response = await _client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(jsonResponse);
                }
                else
                {
                    Console.WriteLine($"HTTP 請求失敗，狀態碼：{response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤：{ex.Message}");
            }
        }

        public async Task<string> PostAsync(string route, object param)
        {
            try
            {
                // Define the URL you want to send the POST request to
                string apiUrl = $"{_url}/{route}";

                // Serialize the data to JSON (you can use a library like Newtonsoft.Json)
                string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(param);

                // Create a StringContent object with the JSON data
                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                // Send the POST request
                HttpResponseMessage response = await _client.PostAsync(apiUrl, content);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return responseContent;
                }
                else
                {
                    Console.WriteLine("HTTP POST request failed with status code: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"發生錯誤：{ex.Message}");
            }

            return string.Empty;
        }
    }
}
