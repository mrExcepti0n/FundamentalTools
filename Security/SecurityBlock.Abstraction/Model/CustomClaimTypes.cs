using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBlock.Abstraction.Model
{
    public static class CustomClaimTypes
    {
        const string claimBase = @"http://siteName/claims/";
        public const string provider = @"http://identityserver.thinktecture.com/claims/identityprovider";


        public const string AdminAccount = claimBase + "adminAccount";
        public const string FirstName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        public const string LastName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
        public const string Patronymic = "MiddleName";


        public const string Snils = "Snils";
        public const string LegalEntityInformation = claimBase + @"legalEntity";


        public const string IsAdminOfOrg = claimBase + "isAdminOfOrg";
        public const string SettlementList = claimBase + "settlements";
        public const string CurrentLegalEntityOGRN = claimBase + "currentLeOgrn";
        public const string CurrentLegalEntityId = claimBase + "currentLeId";
        public const string WorkerId = claimBase + "WorkerId";
        public const string SpecialRoleType = claimBase + "SpecialRoleType";

        public const string AdminWorkerId = claimBase + "adminWorkerId";

        public const string SecutiryAccessRight = claimBase + "secutiryAccessRight";

        public const string SiteName = "SiteName";
        public const string LoginType = "LoginType";
    }
}
