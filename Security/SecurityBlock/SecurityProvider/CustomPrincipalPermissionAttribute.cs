using SecurityBlock.Abstraction.IdentityProvider;
using SecurityBlock.Abstraction.Model;
using System;
using System.Security;
using System.Security.Permissions;

namespace SecurityBlock.SecurityProvider
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomPrincipalPermissionAttribute : CodeAccessSecurityAttribute
    {
        private SecurityAccessRule _securityAccessRule;

        public CustomPrincipalPermissionAttribute(SecurityAction action) : base(action)
        {
            _securityAccessRule = new SecurityAccessRule();
        }

        public SecurityAccessObjectEnum AccessObject
        {
            get => _securityAccessRule.AccessObject;
            set => _securityAccessRule.AccessObject = value;
        }

        public SecurityAccessActionEnum AccessAction
        {
            get => _securityAccessRule.Action; 
            set => _securityAccessRule.Action = value; 
        }

        public override IPermission CreatePermission()
        {
            IPermission perm = new CustomPermission(_securityAccessRule);
            return perm;
        }
    }
}
