using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Web;

namespace SoftNews.Tests
{
    public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var values = Request.Headers["Authorization"].FirstOrDefault();

            AuthenticateResult result;

            if (values == null)
            {
                result = AuthenticateResult.Fail("Missing auth header");
                return Task.FromResult(result);
            }

            var requestValues = values.Substring("Test ".Length);
            var parsedRequestValues = HttpUtility.ParseQueryString(requestValues);

            List<Claim> claims = new List<Claim>();

            if (!string.IsNullOrEmpty(parsedRequestValues["nameIdentifier"]))
                claims.Add(new Claim(ClaimTypes.NameIdentifier, parsedRequestValues["nameIdentifier"]));
            if (!string.IsNullOrEmpty(parsedRequestValues["name"]))
                claims.Add(new Claim(ClaimTypes.Name, parsedRequestValues["name"]));

            var identity = new ClaimsIdentity(claims, "Test");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "Test");

            result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }
}
