using SecurityBlock.Abstraction.Model;
using System.Collections.Generic;
using System.Security.Claims;


namespace SecurityBlock.Abstraction.IdentityProvider
{
    public interface IIdentityProvider
    {

        IEnumerable<Claim> Claims { get; }

        int? LegalEntityId { get; }
        string LegalEntityOGRN { get; }

        int? SpecialRoleType { get; }
        int? WorkerId { get; }
        bool? IsAdminOfCurrentOrganization { get; }
        int? AdminWorkerId { get; }
        string AdminAccount { get; }

        string FirstName { get; }
        string LastName { get; }
        string Patronymic { get; }
        string FIO { get; }

        IEnumerable<int> SettlementIdCollection { get; }

        IEnumerable<SecurityAccessRule> SecurityAccessRights { get; }
        IEnumerable<Organization> Organizations { get; }

        bool HasAccess(params SecurityAccessRule[] securityAccessRules);

        bool NeedDocumentForSaveOperation(SecurityAccessRule rule);

        bool HasAccessToPersonalData();

    }
}
