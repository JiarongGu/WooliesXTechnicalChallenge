﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXTechnicalChallenge.Enums;
using WooliesXTechnicalChallenge.Models;

namespace WooliesXTechnicalChallenge.Services
{
    public interface IResourceService
    {
        IEnumerable<ShopperHistory> GetShopperHisotry(string token);

        IEnumerable<Product> GetProducts(string token);

        decimal GetTrolleyCalculator(Trolley request, string token);
    }
}
