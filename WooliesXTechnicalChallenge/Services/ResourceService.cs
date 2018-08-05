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
        private readonly TesterSettings _testerSettings;

        public ResourceService(
            IHttpClientFactory httpClientFactory, 
            IQueryBuilderService queryBuilderService, 
            IOptions<ResourceApiSettings> apiSettings,
            IOptions<QuerySettings> querySettings,
            IOptions<TesterSettings> testerSettings
            )
        {
            _httpClientFactory = httpClientFactory;
            _queryBuilderService = queryBuilderService;
            _apiSettings = apiSettings.Value;
            _querySettings = querySettings.Value;
            _testerSettings = testerSettings.Value;
        }
        
        public ShopperHistory GetShopperHisotry() {
            return GetResourceHttpClient().Get<ShopperHistory>($"{_apiSettings.ShooperHistoryUri}?{GetTokenQuery()}");
        }

        public IEnumerable<Product> GetProducts() {
            return GetResourceHttpClient().Get<IEnumerable<Product>>($"{_apiSettings.ProductsUri}?{GetTokenQuery()}");
        }

        private HttpClient GetResourceHttpClient() {
            return _httpClientFactory.CreateClient(_apiSettings.Key);
        }

        private string GetTokenQuery() {
            var tokenParameter = new Dictionary<string, string>();
            tokenParameter[_querySettings.Token] = _testerSettings.Token;

            return _queryBuilderService.BuildQuery(tokenParameter);
        }
    }
}
