using SecurityBlock.Abstraction.IdentityProvider;
using SecurityBlock.Abstraction.Model;
using SecurityBlock.Abstraction.SecurityProvider;
using SecurityBlock.IdentityProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SecurityBlock.Tests
{
    public class CustomPermissionTest
    {
        [Fact]
        public void Intersect_UnionNotEpmtyPremission_ReturnResult()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void Demand_AccessRWRequiredR_WithoutExcpetion()
        {
            var secirityAccessRights = new List<SecurityAccessRule> {
                new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW)
            };

            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecutiryAccessRights(secirityAccessRights));

            var customPermission = new CustomPermission(new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.R));
            customPermission.Demand();
        }


        [Fact]
        public void Demand_AccessRRequiredRW_WithExcpetion()
        {
            var secirityAccessRights = new List<SecurityAccessRule> {
                new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.R)
            };

            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecutiryAccessRights(secirityAccessRights));

            var customPermission = new CustomPermission(new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW));
            Assert.Throws<UnauthorizedAccessException>(() => customPermission.Demand());
        }
    }
}
