using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using WooliesXTechnicalChallenge.Options;

namespace WooliesXTechnicalChallenge.Handlers
{
    public class TokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly TesterSettings _testerSettings;
        private readonly QuerySettings _querySettings;

        public TokenAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock,
            IOptions<TesterSettings> testerSettings,
            IOptions<QuerySettings> querySettings
        )
            : base(options, logger, encoder, clock)
        {
            _testerSettings = testerSettings.Value;
            _querySettings = querySettings.Value;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (Context?.Request?.Query[_querySettings.Token].ToString() != _testerSettings.Token)
            {
                return AuthenticateResult.Fail("Unauthorized Access");
            }
            
            var claims = new[] { new Claim(ClaimTypes.Name, _testerSettings.Name) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
    }
}
