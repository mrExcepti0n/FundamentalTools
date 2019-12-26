using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using SecurityBlock.Abstraction.IdentityProvider;

namespace SecurityBlock.IdentityProvider
{
    public class IdentityProvider : AbstractClaimsIdentityProvider
    {
        public override IEnumerable<Claim> Cliams => (Thread.CurrentPrincipal as ClaimsPrincipal).Claims;
       
    }
}
