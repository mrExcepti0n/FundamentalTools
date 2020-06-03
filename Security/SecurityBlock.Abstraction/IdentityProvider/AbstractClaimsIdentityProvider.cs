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

        public override int? LegalEntityId => Claims.GetLegalEntityId();
        public override string LegalEntityOGRN => Claims.GetLegalEntityOgrn();
        public override int? SpecialRoleType => Claims.GetSpecialRoleType();

        public override int? WorkerId => Claims.GetWorkerId();

        public override bool? IsAdminOfCurrentOrganization => Claims.IsAdminOfCurrentOrganization();

        public override int? AdminWorkerId => Claims.GetAdminWorkerId();
        public override string AdminAccount => Claims.GetAdminAccount();
        public override string FirstName => Claims.GetFirstName(); 
        public override string LastName => Claims.GetLastName(); 
        public override string Patronymic => Claims.GetPatronymic(); 


        public override IEnumerable<int> SettlementIdCollection => Claims.GetSettlements(); 

        public override IEnumerable<SecurityAccessRule> SecurityAccessRights => Claims.GetSecurityRights(); 
        public override IEnumerable<Organization> Organizations  => Claims.GetOrganizations(); 
    
    }
}
