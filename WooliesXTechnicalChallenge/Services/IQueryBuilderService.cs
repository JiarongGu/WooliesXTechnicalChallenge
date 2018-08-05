using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WooliesXTechnicalChallenge.Services
{
    public interface IQueryBuilderService
    {
        string BuildQuery(IDictionary<string, string> keyValues);
    }
}
