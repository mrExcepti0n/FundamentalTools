using SecurityBlock.Abstraction.IdentityProvider;
using SecurityBlock.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityBlock.SecurityProvider;
using Xunit;

namespace SecurityBlock.Tests
{
    public class CustomPermissionTest
    {
        [Fact]
        public void Intersect_UnionNotEmptyPermission_ReturnResult()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Demand_AccessRWRequiredR_WithoutException()
        {
            var securityAccessRights = new [] {
                new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW)
            };

            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecurityAccessRights(securityAccessRights));

            var customPermission = new CustomPermission(new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.R));
            customPermission.Demand();
        }


        [Fact]
        public void Demand_AccessRRequiredRW_WithException()
        {
            var securityAccessRights = new [] {
                new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.R)
            };

            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecurityAccessRights(securityAccessRights));

            var customPermission = new CustomPermission(new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW));
            Assert.Throws<UnauthorizedAccessException>(() => customPermission.Demand());
        }
    }
}
