using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using SecurityBlock.Abstraction.IdentityProvider;

namespace SecurityBlock
{
    public class IdentityProvider : AbstractClaimsIdentityProvider
    {
        public override IEnumerable<Claim> Claims => (Thread.CurrentPrincipal as ClaimsPrincipal).Claims;
       
    }
}
