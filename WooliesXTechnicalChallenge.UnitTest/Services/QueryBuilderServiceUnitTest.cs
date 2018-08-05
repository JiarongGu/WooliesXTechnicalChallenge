using System;
using System.Collections.Generic;
using System.Text;
using WooliesXTechnicalChallenge.Services;
using Xunit;

namespace WooliesXTechnicalChallenge.UnitTest.Services
{
    public class QueryBuilderServiceUnitTest
    {
        [Fact]
        public void Build_Should_Handle_Null()
        {
            var build = new QueryBuilderService();
            var query = build.BuildQuery(null);

            Assert.Equal(string.Empty, query);
        }

        [Fact]
        public void Build_Should_Handle_Single_Parameter() {
            var build = new QueryBuilderService();

            var parameters = new Dictionary<string, string>();
            parameters["token"] = "c72813cd-9879-436a-9a6d-9497e2cf5771";

            var query = build.BuildQuery(parameters);

            Assert.Equal("token=c72813cd-9879-436a-9a6d-9497e2cf5771", query);
        }

        [Fact]
        public void Build_Should_Handle_Multiple_Parameters()
        {
            var build = new QueryBuilderService();

            var parameters = new Dictionary<string, string>();
            parameters["token"] = "c72813cd-9879-436a-9a6d-9497e2cf5771";
            parameters["customerId"] = "1";

            var query = build.BuildQuery(parameters);

            Assert.Equal("token=c72813cd-9879-436a-9a6d-9497e2cf5771&customerId=1", query);
        }
    }
}
