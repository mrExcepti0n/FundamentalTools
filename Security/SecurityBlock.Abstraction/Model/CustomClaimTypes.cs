using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityBlock.Abstraction.Model
{
    public static class CustomClaimTypes
    {
        const string ClaimBase = @"http://siteName/claims/";
        public const string Provider = @"http://identityserver.thinktecture.com/claims/identityprovider";


        public const string AdminAccount = ClaimBase + "adminAccount";
        public const string FirstName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname";
        public const string LastName = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname";
        public const string Patronymic = "MiddleName";


        public const string Snils = "Snils";
        public const string LegalEntityInformation = ClaimBase + @"legalEntity";


        public const string IsAdminOfOrg = ClaimBase + "isAdminOfOrg";
        public const string SettlementList = ClaimBase + "settlements";
        public const string CurrrentLegalEntityOGRN = ClaimBase + "currentLeOgrn";
        public const string CurrentLegalEntityId = ClaimBase + "currentLeId";
        public const string WorkerId = ClaimBase + "WorkerId";
        public const string SpecialRoleType = ClaimBase + "SpecialRoleType";

        public const string AdminWorkerId = ClaimBase + "adminWorkerId";

        public const string SecurityAccessRight = ClaimBase + "secutiryAccessRight";

        public const string SiteName = "SiteName";
        public const string LoginType = "LoginType";
    }
}
