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

        /// <summary>
        ///     Initializes a new instance of the <see cref="HttpDataHelper"/> class
        /// </summary>
        /// <param name="baseAddress">Base url to te endpoint</param>
        /// <param name="accessToken">Bearer token for authentication</param>
        /// <param name="headers">Headers for the request as a tuple</param>
        public HttpDataHelper(string baseAddress, string accessToken, List<(string name, string value)> headers)
        {
            // initialise http client
           Client = new HttpClient();

            // set the base address
            Client.BaseAddress = new Uri(baseAddress);

            // add the access token for authentication
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            //add the request headers
            foreach (var (name, value) in headers)
            {
                Client.DefaultRequestHeaders.Add(name, value);
            }
        }

        /// <summary>
        ///     A get request to fetch items from a specified endpoint
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public async Task<ResponseMessage> GetItemsAsync(string endpoint)
        {
            var response = await Client.GetAsync(endpoint);
            var responseString = await response.Content.ReadAsStringAsync();
            var returnObj = JsonConvert.DeserializeObject(responseString);

            return new ResponseMessage(response.IsSuccessStatusCode, response.StatusCode, returnObj);
        }

        /// <summary>
        ///     A post request to a specified endpoint.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<ResponseMessage> PostItemAsync<T>(string endpoint, T item)
        {
            var stringItem = JsonConvert.SerializeObject(item);
            var content = new StringContent(stringItem, Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(endpoint, content);

            var responseString = await response.Content.ReadAsStringAsync();
            var returnObj = JsonConvert.DeserializeObject(responseString);

            return new ResponseMessage(response.IsSuccessStatusCode, response.StatusCode, returnObj);
        }
    }
}
