using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WooliesXTechnicalChallenge.Enums;
using WooliesXTechnicalChallenge.Extensions;
using WooliesXTechnicalChallenge.Models;
using WooliesXTechnicalChallenge.Options;

namespace WooliesXTechnicalChallenge.Services
{
    public class ResourceService: IResourceService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IQueryBuilderService _queryBuilderService;
        private readonly ResourceApiSettings _apiSettings;
        private readonly QuerySettings _querySettings;

        public ResourceService(
            IHttpClientFactory httpClientFactory, 
            IQueryBuilderService queryBuilderService, 
            IOptions<ResourceApiSettings> apiSettings,
            IOptions<QuerySettings> querySettings
            )
        {
            _httpClientFactory = httpClientFactory;
            _queryBuilderService = queryBuilderService;
            _apiSettings = apiSettings.Value;
            _querySettings = querySettings.Value;
        }
        
        public ShopperHistory GetShopperHisotry(string token) {
            return GetResourceHttpClient()
                .Get<ShopperHistory>($"{_apiSettings.ShooperHistoryUri}?{GetTokenQuery(token)}");
        }

        public IEnumerable<Product> GetProducts(string token)
        {
            return GetResourceHttpClient()
                .Get<IEnumerable<Product>>($"{_apiSettings.ProductsUri}?{GetTokenQuery(token)}");
        }

        private HttpClient GetResourceHttpClient() {
            return _httpClientFactory.CreateClient(_apiSettings.Key);
        }

        private string GetTokenQuery(string token) {
            var tokenParameter = new Dictionary<string, string>();
            tokenParameter[_querySettings.Token] = token;

            return _queryBuilderService.BuildQuery(tokenParameter);
        }
    }
}
