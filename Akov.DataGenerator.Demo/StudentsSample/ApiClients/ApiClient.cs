using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Akov.DataGenerator.Demo.StudentsSample.Responses;
using Newtonsoft.Json;

namespace Akov.DataGenerator.Demo.StudentsSample.ApiClients
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _parsingSettings;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _parsingSettings = new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yy",
                Culture = new CultureInfo("en-US"),
            };
        }

        public async Task<T?> GetAsync<T>(string url) where T : class
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseAsString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(responseAsString, _parsingSettings);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return default;
            }
        }
    }
}
