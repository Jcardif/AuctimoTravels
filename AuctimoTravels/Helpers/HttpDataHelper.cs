using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AuctimoTravels.Models;
using Newtonsoft.Json;

namespace AuctimoTravels.Helpers
{
    public class HttpDataHelper
    {
        private HttpClient Client { get; set; }
        public HttpDataHelper(string baseAddress, string authToken, List<(string key, string value)> headers)
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(baseAddress);

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            foreach (var (key, value) in headers)
            {
                Client.DefaultRequestHeaders.Add(key, value);
            }
        }

        public async Task<ResponseMessage> GetItem<T>(string endpoint)
        {
            var response = await Client.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode) 
                return default;

            var returnContent = await response.Content.ReadAsStringAsync();
            return new ResponseMessage(returnContent, response.StatusCode, response.IsSuccessStatusCode);
        }

        public async Task<ResponseMessage> PostItem<T>(T item, string endpoint)
        {
            var serializedItem = JsonConvert.SerializeObject(item);
            var content = new StringContent(serializedItem, Encoding.UTF8, "application/json");

            var response = await Client.PostAsync(endpoint, content);

            var returnContent = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return new ResponseMessage(returnContent, response.StatusCode, response.IsSuccessStatusCode);

        }


    }
}
