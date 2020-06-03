using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SecurityBlock.Abstraction.IdentityProvider;

namespace SecurityBlockCore
{
    public class BillingIdentityProvider : AbstractClaimsIdentityProvider
    {
        public BillingIdentityProvider(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }

        private readonly HttpContext _httpContext;

        public override IEnumerable<Claim> Claims => _httpContext.User.Claims;

    }
}
