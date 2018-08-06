using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WooliesXTechnicalChallenge.Enums;
using WooliesXTechnicalChallenge.Models;
using WooliesXTechnicalChallenge.Options;

namespace WooliesXTechnicalChallenge.Services
{
    public interface IAnswersService
    {
        IEnumerable<Product> GetProducts(SortOptions sortOption);

        TesterSettings GetTesterSettings();
    }
}
