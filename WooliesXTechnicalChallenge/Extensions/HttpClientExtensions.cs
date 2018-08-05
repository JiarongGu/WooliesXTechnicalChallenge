using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Extensions
{
    public static class HttpClientExtensions
    {
        public static T Get<T>(this HttpClient httpClient, string requestUri) {
            var result = httpClient.GetAsync(requestUri).Result;

            if (!result.IsSuccessStatusCode)
                throw new HttpRequestException($"external request failed./n{result.ReasonPhrase}");

            var json = result.Content.ReadAsStringAsync().Result;
            var objects = JsonConvert.DeserializeObject<T>(json);

            return objects;
        }
    }
}
