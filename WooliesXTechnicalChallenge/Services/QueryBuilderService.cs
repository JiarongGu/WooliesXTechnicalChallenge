using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace WooliesXTechnicalChallenge.Services
{
    public class QueryBuilderService: IQueryBuilderService
    {
        public string BuildQuery(IDictionary<string, string> keyValues) {
            if (keyValues == null)
                return string.Empty;

            var query = HttpUtility.ParseQueryString(string.Empty);
            
            foreach (var keyValue in keyValues) {
                query[keyValue.Key] = keyValue.Value;
            }
            return query.ToString();
        }
    }
}
