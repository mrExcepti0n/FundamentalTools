using SecurityBlock.Abstraction.IdentityProvider;
using SecurityBlock.Abstraction.Model;
using SecurityBlock.IdentityProvider;
using SecurityBlock.SecurityProvider;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Xunit;

namespace SecurityBlock.Tests
{
    public class CustomPrincipalPremissionAttributeTest
    {
        [Fact]
        public void CheckAccess_WithoutRights_ThrowsException()
        {
            IdentityProviderFactory.Init(new TestIdentityProvider());
            var exception = Assert.Throws<UnauthorizedAccessException>(() => new SomeClass().SomeMethod());
        }

        [Fact]
        public void CheckAccess_WithRights_ReturnValue()
        {
            var secirityAccessRights = new List<SecurityAccessRule> { new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW) };
            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecutiryAccessRights(secirityAccessRights));

            var res = new SomeClass().SomeMethod();
            Assert.NotNull(res);
        }

        [Fact]
        public void CheckAccess_WithRightsDoublePermision_ReturnValue()
        {
            var secirityAccessRights = new List<SecurityAccessRule> {
                new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW),
                new SecurityAccessRule(SecurityAccessObjectEnum.Report, SecurityAccessActionEnum.RW),
            };

            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecutiryAccessRights(secirityAccessRights));

            var res = new SomeClass().SomeMethod2();
            Assert.NotNull(res);
        }


        [Fact]
        public void CheckAccess_WithoutFullRightsDoublePermision_ThrowExcpetion()
        {
            var secirityAccessRights = new List<SecurityAccessRule> {
                new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW)
            };

            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecutiryAccessRights(secirityAccessRights));

            var exception = Assert.Throws<UnauthorizedAccessException>(() => new SomeClass().SomeMethod2());
        }



    }
}
