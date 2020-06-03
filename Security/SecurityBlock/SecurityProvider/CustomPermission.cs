using System;
using System.Linq;
using System.Security;
using SecurityBlock.Abstraction.Model;

namespace SecurityBlock.SecurityProvider
{
    public class CustomPermission : IPermission
    {

        private readonly SecurityAccessRule[] _securityAccessRules;
        public CustomPermission(SecurityAccessRule accessRule)
        {
            _securityAccessRules = new[] { accessRule };
        }

        private CustomPermission(SecurityAccessRule[] accessRules)
        {
            _securityAccessRules = accessRules;
        }

        public IPermission Copy()
        {
            return new CustomPermission(_securityAccessRules);
        }

        public void Demand()
        {
            if (!IdentityProviderFactory.Get.HasAccess(_securityAccessRules))
            {
                ThrowSecurityException();
            }
        }


        [SecurityCritical]
        private void ThrowSecurityException()
        {
            throw new UnauthorizedAccessException(); ;
        }

        public void FromXml(SecurityElement elem)
        {
            throw new NotImplementedException();
        }

        public IPermission Intersect(IPermission target)
        {
            if (target == null)
            {
                return null;
            }
            else if (!VerifyType(target))
            {
                throw new ArgumentException();
            }
            else if (this.IsUnrestricted())
            {
                return target.Copy();
            }

            CustomPermission operand = (CustomPermission)target;

            if (operand.IsUnrestricted())
            {
                return this.Copy();
            }

            throw new NotImplementedException();        
        }




        public bool IsSubsetOf(IPermission target)
        {
            if (target == null)
            {
                return IsEmpty();
            }

            return false;
        }

        public SecurityElement ToXml()
        {
            throw new NotImplementedException();
        }

        public IPermission Union(IPermission target)
        {
            throw new NotImplementedException();
        }


        private bool VerifyType(IPermission perm)
        {
            if ((perm == null) || (perm.GetType() != GetType()))
            {
                return (false);
            }
            else
            {
                return (true);
            }
        }

        private bool IsEmpty()
        {
            return IsUnrestricted();
        }

        public bool IsUnrestricted()
        {
            if (!_securityAccessRules.Any())
            {
                return true;
            }
            return false;
        }


    }
}
