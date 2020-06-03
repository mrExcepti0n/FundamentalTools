using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using SecurityBlock.Abstraction.Extensions;
using SecurityBlock.Abstraction.Model;

namespace SecurityBlock.Abstraction.IdentityProvider
{
    public abstract class AbstractIdentityProvider : IIdentityProvider
    {
        public abstract IEnumerable<Claim> Claims { get; }

        public abstract int? LegalEntityId { get;  }

        public abstract string LegalEntityOGRN { get; }

        public abstract int? SpecialRoleType { get; }

        public abstract int? WorkerId { get; }

        public abstract bool? IsAdminOfCurrentOrganization { get; }

        public abstract int? AdminWorkerId { get; }

        public abstract string AdminAccount { get; }
        public abstract string FirstName { get; }
        public abstract string LastName { get; }
        public abstract string Patronymic { get; }
        public string FIO => $"{FirstName} {LastName} {Patronymic}";

        public abstract IEnumerable<int> SettlementIdCollection { get; }

        public abstract IEnumerable<SecurityAccessRule> SecurityAccessRights { get; }

        public abstract IEnumerable<Organization> Organizations { get; }

        public virtual bool HasAccess(params SecurityAccessRule[] securityAccessRules)
        {
            foreach (var securityAccessRule in securityAccessRules)
            {
                var accessRight = SecurityAccessRights.SingleOrDefault(sar => sar.AccessObject == securityAccessRule.AccessObject);
                if (accessRight == null)
                {
                    return false;
                }
                if (accessRight.Action.LessThan(securityAccessRule.Action))
                {
                    return false;
                }
            }
            return true;
        }


        public virtual bool NeedDocumentForSaveOperation(SecurityAccessRule rule)
        {
            if ((rule.Action & SecurityAccessActionEnum.WWD) == 0)
            {
                return false;
            }
            var rights = SecurityAccessRights.SingleOrDefault(sar => sar.AccessObject == rule.AccessObject);

            return rights != null && rights.Action.NeedDocument();
        }

        public virtual bool HasAccessToPersonalData()
        {
            return SecurityAccessRights.SingleOrDefault(sar => sar.AccessObject == SecurityAccessObjectEnum.PersonalData) != null;
        }

    }
  
}
