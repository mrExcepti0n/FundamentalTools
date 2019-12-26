using SecurityBlock.Abstraction.Model;
using SecurityBlock.Abstraction.Tools;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SecurityBlock.Abstraction.IdentityProvider
{
    public abstract class AbstractClaimsIdentityProvider : AbstractIdentityProvider
    {

        public override int? LegalEntityId { get => Cliams.GetLegalEntityId(); }
        public override string LegalEntityOGRN { get => Cliams.GetLegalEntityOgrn(); }
        public override int? SpecialRoleType { get => Cliams.GetSpecialRoleType(); }

        public override int? WorkerId { get => Cliams.GetWorkerId(); }

        public override bool? IsAdminOfCurentOrganization { get => Cliams.IsAdminOfCurrentOrganization(); }

        public override int? AdminWorkerId { get => Cliams.GetAdminWorkerId(); }
        public override string AdminAccount { get => Cliams.GetAdminAccount(); }
        public override string FirstName { get => Cliams.GetFirstName(); }
        public override string LastName { get => Cliams.GetLastName(); }
        public override string Patronymic { get => Cliams.GetPatronymic(); }


        public override IEnumerable<int> SettlementIdCollection { get => Cliams.GetSettlements(); }

        public override IEnumerable<SecurityAccessRule> SecurityAccessRights { get => Cliams.GetSecurityRights(); }
        public override IEnumerable<Organization> Organizations { get => Cliams.GetOrganizations(); }
    
    }
}
