using Microsoft.AspNetCore.Http;
using SecurityBlock.Abstraction.IdentityProvider;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SecurityBlockCore.IdentityProvider
{
    public class BillingIdentityProvider : AbstractClaimsIdentityProvider
    {
        public BillingIdentityProvider(HttpContext httpContenxt)
        {
            _httpContext = httpContenxt;
        }

        private HttpContext _httpContext;

        public override IEnumerable<Claim> Cliams => _httpContext.User.Claims;

    }
}
