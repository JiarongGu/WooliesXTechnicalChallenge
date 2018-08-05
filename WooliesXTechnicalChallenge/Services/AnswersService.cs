using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXTechnicalChallenge.Enums;
using WooliesXTechnicalChallenge.Models;
using WooliesXTechnicalChallenge.Options;

namespace WooliesXTechnicalChallenge.Services
{
    public class AnswersService : IAnswersService
    {
        private readonly IResourceService _resourceService;
        private readonly TesterSettings _testerSettings;

        public AnswersService(IResourceService resourceService, IOptions<TesterSettings> testerSettings)
        {
            _resourceService = resourceService;
            _testerSettings = testerSettings.Value;
        }

        public IEnumerable<Product> GetProducts(SortOptions sortOption)
        {
            var token = _testerSettings.Token;

            if (sortOption == SortOptions.Recommended)
                return _resourceService.GetShopperHisotry(token)
                    .SelectMany(x => x.Products)
                    .GroupBy(x => (x.Name, x.Price))
                    .Select(x => x.First());

            var products = _resourceService.GetProducts(token);

            if (sortOption == SortOptions.Low)
                return products.OrderBy(x => x.Price);

            if (sortOption == SortOptions.High)
                return products.OrderByDescending(x => x.Price);

            if (sortOption == SortOptions.Descending)
                return products.OrderByDescending(x => x.Name);

            if (sortOption == SortOptions.Ascending)
                return products.OrderBy(x => x.Name);

            return null;
        }

        public TesterSettings GetTesterSettings()
        {
            return _testerSettings;
        }

        public decimal GetTrolleyCalculatorRemote(TrolleyCalculatorRequest request)
        {
            return _resourceService.GetTrolleyCalculator(request, _testerSettings.Token);
        }

        public decimal GetTrolleyCalculatorLocal(TrolleyCalculatorRequest request)
        {
            var productTotalPrice = request.Products.Sum(x => x.Price);

            var sortedSpecials = request.Specials?.Where(x => x.Total > 0).OrderBy(x => x.UnitPrice);

            var quntities = request.Quantities.FirstOrDefault().Value;

            var totalPrice = 0m;

            if (sortedSpecials != null)
            {
                foreach (var special in sortedSpecials)
                {
                    if (special.UnitPrice < productTotalPrice)
                    {
                        var specialUnits = (int)(quntities / special.Quantity);
                        quntities = quntities % special.Quantity;

                        totalPrice += specialUnits * special.Total;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            totalPrice += quntities * productTotalPrice;

            return totalPrice;
        }
    }
}
