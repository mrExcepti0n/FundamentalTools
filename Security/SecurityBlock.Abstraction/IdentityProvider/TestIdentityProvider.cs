using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using SecurityBlock.Abstraction.Model;

namespace SecurityBlock.Abstraction.IdentityProvider
{
    public class TestIdentityProvider : AbstractIdentityProvider
    {
        public TestIdentityProvider()
        {
            _legalEntityId = 1;
            _workerId = 1;
            _settlementIdCollection = new List<int>();
            _securityAccessRights = new List<SecurityAccessRule>();
            _organizations = new List<Organization>();
            _firstName = "Иванов";
            _lastName = "Иван";
            _patronymic = "Иванович";
            _isAdminOfCurrentOrganization = false;

        }

        private int? _legalEntityId;

        public override int? LegalEntityId => _legalEntityId;

        private int? _workerId;
        public override int? WorkerId => _workerId;

        private IEnumerable<int> _settlementIdCollection;

        public override IEnumerable<int> SettlementIdCollection => _settlementIdCollection;

        private IEnumerable<SecurityAccessRule> _securityAccessRights;
        public override IEnumerable<SecurityAccessRule> SecurityAccessRights => _securityAccessRights;


        private string _legalEntityOGRN;
        public override string LegalEntityOGRN => _legalEntityOGRN;

        private IEnumerable<Organization> _organizations;
        public override IEnumerable<Organization> Organizations => _organizations;
        public override IEnumerable<Claim> Claims => new List<Claim>();


        private int? _specialRoleType;
        public override int? SpecialRoleType => _specialRoleType;

        private bool? _isAdminOfCurrentOrganization;
        public override bool? IsAdminOfCurrentOrganization => _isAdminOfCurrentOrganization;

        private int? _adminWorkerId;
        public override int? AdminWorkerId => _adminWorkerId;

        private string _adminAccount;
        public override string AdminAccount => _adminAccount;


        private string _firstName;
        public override string FirstName => _firstName;
        private string _lastName;
        public override string LastName => _lastName;
        private string _patronymic;
        public override string Patronymic => _patronymic;

        public TestIdentityProvider WithLegalEntityId(int legalEntityId)
        {
            _legalEntityId = legalEntityId;
            return this;
        }

        public TestIdentityProvider WithWorkerId(int workerId)
        {
            _workerId = workerId;
            return this;
        }

        public TestIdentityProvider WithSettlementIdCollection(IEnumerable<int> settlementIdCollection)
        {
            _settlementIdCollection = settlementIdCollection;
            return this;
        }

        public TestIdentityProvider WithAllRights()
        {
            var securityRights = Enum.GetValues(typeof(SecurityAccessObjectEnum))
                                    .Cast<SecurityAccessObjectEnum>()
                                    .Select(sao => new SecurityAccessRule(sao, SecurityAccessActionEnum.RW));

            return WithSecurityAccessRights(securityRights.ToArray());
        }

        public TestIdentityProvider WithSecurityAccessRights(params SecurityAccessRule[] securityAccessRights)
        {
            _securityAccessRights = securityAccessRights;
            return this;
        }

        public TestIdentityProvider WithLegalEntityOGRN(string legalEntityOgrn)
        {
            _legalEntityOGRN = legalEntityOgrn;
            return this;
        }

        public TestIdentityProvider WithOrganizations(IEnumerable<Organization> organizations)
        {
            _organizations = organizations;
            return this;
        }

        public TestIdentityProvider WithSpecialRoleType(int specialRoleType)
        {
            _specialRoleType = specialRoleType;
            return this;
        }

        public TestIdentityProvider WithIsAdminOfCurrentOrganization(bool isAdminOfCurrentOrganization)
        {
            _isAdminOfCurrentOrganization = isAdminOfCurrentOrganization;
            return this;
        }
        public TestIdentityProvider WithAdminWorkerId(int? adminWorkerId)
        {
            _adminWorkerId = adminWorkerId;
            return this;
        }
        public TestIdentityProvider WithAdminAccount(string adminAccount)
        {
            _adminAccount = adminAccount;
            return this;
        }

        public TestIdentityProvider WithPersonalDataAccess(bool hasAccess = true)
        {
            if (!hasAccess)
            {
                _securityAccessRights = _securityAccessRights.Where(sar => sar.AccessObject != SecurityAccessObjectEnum.PersonalData).ToArray();
            }
            else if (_securityAccessRights.All(sar => sar.AccessObject != SecurityAccessObjectEnum.PersonalData))
            {
                _securityAccessRights = _securityAccessRights.Union(new SecurityAccessRule[] {
                    new SecurityAccessRule(SecurityAccessObjectEnum.PersonalData, SecurityAccessActionEnum.RW)
                }).ToArray();
            }
            return this;
        }

    }
}
