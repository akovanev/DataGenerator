using System;
using System.Diagnostics;
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
                Error = (sender, args) =>
                {
                    var result = args.CurrentObject as Result;
                    result?.ParsingErrors.AppendLine(args.ErrorContext.Error.Message);
                    args.ErrorContext.Handled = true;
                }
            };
        }

        public async Task<T?> GetAsync<T>(string url) where T : Result
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
