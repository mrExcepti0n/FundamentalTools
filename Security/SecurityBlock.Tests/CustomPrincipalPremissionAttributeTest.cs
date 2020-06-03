using SecurityBlock.Abstraction.IdentityProvider;
using SecurityBlock.Abstraction.Model;
using SecurityBlock.SecurityProvider;
using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Xunit;

namespace SecurityBlock.Tests
{
    public class CustomPrincipalPermissionAttributeTest
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
            var securityAccessRights = new [] { new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW) };
            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecurityAccessRights(securityAccessRights));

            var res = new SomeClass().SomeMethod();
            Assert.NotNull(res);
        }

        [Fact]
        public void CheckAccess_WithRightsDoublePermission_ReturnValue()
        {
            var securityAccessRights = new [] {
                new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW),
                new SecurityAccessRule(SecurityAccessObjectEnum.Report, SecurityAccessActionEnum.RW),
            };

            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecurityAccessRights(securityAccessRights));

            var res = new SomeClass().SomeMethod2();
            Assert.NotNull(res);
        }


        [Fact]
        public void CheckAccess_WithoutFullRightsDoublePermission_ThrowException()
        {
            var securityAccessRights = new [] {
                new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.RW)
            };

            IdentityProviderFactory.Init(new TestIdentityProvider().WithSecurityAccessRights(securityAccessRights));

            var exception = Assert.Throws<UnauthorizedAccessException>(() => new SomeClass().SomeMethod2());
        }



    }
}
