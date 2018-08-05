using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Extensions
{
    public static class HttpClientExtensions
    {
        public static Response Get<Response>(this HttpClient httpClient, string requestUri) {
            var result = httpClient.GetAsync(requestUri).Result;

            if (!result.IsSuccessStatusCode)
                throw new HttpRequestException($"external request failed./n{result.ReasonPhrase}");

            var json = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<Response>(json);

            return response;
        }

        public static Response Post<Request, Response>(this HttpClient httpClient, string requestUri, Request request)
        {
            var result = httpClient.PostAsync(requestUri, request.FormatJsonContent()).Result;

            if (!result.IsSuccessStatusCode)
                throw new HttpRequestException($"external request failed./n{result.ReasonPhrase}");

            var json = result.Content.ReadAsStringAsync().Result;
            var response = JsonConvert.DeserializeObject<Response>(json);

            return response;
        }

        public static HttpContent FormatJsonContent<T>(this T model)
        {
            return new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
        }
    }
}
