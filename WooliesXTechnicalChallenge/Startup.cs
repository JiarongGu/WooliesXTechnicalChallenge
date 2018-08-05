using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using WooliesXTechnicalChallenge.Handlers;
using WooliesXTechnicalChallenge.Options;
using WooliesXTechnicalChallenge.Services;

namespace WooliesXTechnicalChallenge
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var resourceApiSettings = ConfigureSection<ResourceApiSettings>(services);
            var querySettings = ConfigureSection<QuerySettings>(services);
            var testerSettings = ConfigureSection<TesterSettings>(services);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // Add custom authentication scheme
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = querySettings.Token;
                options.DefaultChallengeScheme = querySettings.Token;
            }).AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>(querySettings.Token, options => { });

            services.AddHttpClient(resourceApiSettings.Key, options => {
                options.BaseAddress = new Uri(resourceApiSettings.BaseApiUri);
                options.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            services.AddSingleton<IQueryBuilderService, QueryBuilderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }

        private T ConfigureSection<T>(IServiceCollection services, string name = null) where T : class
        {
            var section = Configuration.GetSection(name ?? typeof(T).Name);
            services.Configure<T>(section);

            return section.Get<T>();
        }
    }
}
