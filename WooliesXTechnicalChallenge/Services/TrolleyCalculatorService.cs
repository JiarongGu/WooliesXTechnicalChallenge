using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXTechnicalChallenge.Models;
using WooliesXTechnicalChallenge.Extensions;

namespace WooliesXTechnicalChallenge.Services
{
    public class TrolleyCalculatorService: ITrolleyCalculatorService
    {
        public decimal CalculateTrolley(Trolley trolley)
        {
            var products = trolley.Products.Clone();

            foreach (var product in products)
            {
                product.Quantity = trolley.Quantities.FirstOrDefault(x => x.Name == product.Name)?.Value ?? 0;
            }

            if (products.Count() == 0)
                return 0m;

            return ProductPrice(products, trolley.Specials);
        }

        private decimal ProductPrice(IEnumerable<Product> products, IEnumerable<Special> specials, int index = 0)
        {
            if (specials == null || !specials.Any())
                return products.Sum(x => x.Price * x.Quantity);
            
            var selectedSpecial = specials.ElementAt(index);

            var otherSpecials = specials.ToList();
            otherSpecials.RemoveAt(index);

            var groupedProducts = products.Select(x => (
                product: x,
                quantity: selectedSpecial.Quantities.FirstOrDefault(y => y.Name == x.Name)?.Value ?? 0
            ));

            var groupedSpecial = groupedProducts.Where(x => x.quantity > 0);
            var specialCount = groupedSpecial.Count() > 0 ? groupedSpecial.Min(x => (int)(x.product.Quantity / x.quantity)): 0;
            var productsLeft = groupedProducts.Select(x => new Product(x.product.Name, x.product.Price, x.product.Quantity - x.quantity * specialCount));

            if(specialCount == 0) 
                return ProductPrice(productsLeft, otherSpecials);

            var totalPrice = (selectedSpecial.Total * specialCount) + ProductPrice(productsLeft, otherSpecials);
            var nextIndex = index + 1;

            if (nextIndex < specials.Count())
            {
                var nextTotalPrice = ProductPrice(products, specials, nextIndex);
                return totalPrice < nextTotalPrice ? totalPrice : nextTotalPrice;
            }

            return totalPrice;
        }
    }
}
