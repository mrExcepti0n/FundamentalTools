using SecurityBlock.Abstraction;
using SecurityBlock.Abstraction.IdentityProvider;
using SecurityBlock.Abstraction.Model;
using System.Collections.Generic;
using Xunit;

namespace SecurityBlock.Tests
{
    public class IdentityExtensionsTest
    {

        [Theory]
        [InlineData(SecurityAccessActionEnum.RW, SecurityAccessActionEnum.R, false)]
        [InlineData(SecurityAccessActionEnum.R, SecurityAccessActionEnum.RW, true)]
        [InlineData(SecurityAccessActionEnum.RW, SecurityAccessActionEnum.W, false)]
        [InlineData(SecurityAccessActionEnum.WWD, SecurityAccessActionEnum.W, false)]
        [InlineData(SecurityAccessActionEnum.W, SecurityAccessActionEnum.W, false)]
        [InlineData(SecurityAccessActionEnum.RWWD, SecurityAccessActionEnum.W, false)]
        [InlineData(SecurityAccessActionEnum.RWWD, SecurityAccessActionEnum.RW, false)]
        public void LessThan(SecurityAccessActionEnum accessRight, SecurityAccessActionEnum accessRule,
            bool expectedResult)
        {
            Assert.Equal(expectedResult, accessRight.LessThan(accessRule));
        }


        [Fact]
        public void NeedDocumentForSaveOperation0()
        {
            var securityRights = new List<SecurityAccessRule> {new SecurityAccessRule
            {
                AccessObject = SecurityAccessObjectEnum.Administration,
                Action = (SecurityAccessActionEnum) 113
            }};

            var securityRule = new SecurityAccessRule(SecurityAccessObjectEnum.Administration, SecurityAccessActionEnum.RW);
            var result = new TestIdentityProvider().WithSecutiryAccessRights(securityRights).HasAccess(securityRule);
            Assert.True(result);
        }



        [Fact]
        public void NeedDocumentForSaveOperation()
        {
            var securityRights = new List<SecurityAccessRule> {new SecurityAccessRule
            {
                AccessObject = SecurityAccessObjectEnum.Agreement,
                Action = SecurityAccessActionEnum.I
            }};

            var securityRule = new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.IWD);

            var result = new TestIdentityProvider().WithSecutiryAccessRights(securityRights)
                .NeedDocumentForSaveOperation(securityRule);
            Assert.False(result);
        }


        [Fact]
        public void NeedDocumentForSaveOperation1()
        {
            var securityRights = new List<SecurityAccessRule> {new SecurityAccessRule
            {
                AccessObject = SecurityAccessObjectEnum.Agreement,
                Action = SecurityAccessActionEnum.IWD
            }};

            var securityRule = new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.WWD);

            var result = new TestIdentityProvider().WithSecutiryAccessRights(securityRights)
                .NeedDocumentForSaveOperation(securityRule);
            Assert.True(result);
        }

        [Fact]
        public void NeedDocumentForSaveOperation2()
        {
            var securityRights = new List<SecurityAccessRule> {new SecurityAccessRule
            {
                AccessObject = SecurityAccessObjectEnum.Agreement,
                Action = SecurityAccessActionEnum.IWD
            }};

            var securityRule = new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.IWD);

            var result = new TestIdentityProvider().WithSecutiryAccessRights(securityRights)
                .NeedDocumentForSaveOperation(securityRule);
            Assert.True(result);
        }

        [Fact]
        public void NeedDocumentForSaveOperation3()
        {
            var securityRights = new List<SecurityAccessRule> { new SecurityAccessRule() };
            var securityRule = new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.I);

            var result = new TestIdentityProvider().WithSecutiryAccessRights(securityRights)
                .NeedDocumentForSaveOperation(securityRule);
            Assert.False(result);
        }
    }
}
