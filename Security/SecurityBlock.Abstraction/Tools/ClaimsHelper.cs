using SecurityBlock.Abstraction.IdentityProvider;
using SecurityBlock.Abstraction.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace SecurityBlock.Abstraction.Tools
{
    public static class ClaimsHelper
    {
        public static string GetClaim(this IEnumerable<Claim> claims, string claimType)
        {
            return claims.FirstOrDefault(x => x.Type == claimType)?.Value;
        }

        public static int? GetWorkerId(this IEnumerable<Claim> claims)
        {
            string workerId = GetClaim(claims, CustomClaimTypes.WorkerId);
            if (!String.IsNullOrEmpty(workerId))
            {
                return Convert.ToInt32(workerId);
            }
            return null;
        }

        public static int? GetLegalEntityId(this IEnumerable<Claim> claims)
        {
            string legalEntityId = claims.GetClaim(CustomClaimTypes.CurrentLegalEntityId);

            if (!String.IsNullOrEmpty(legalEntityId))
            {
                return Convert.ToInt32(legalEntityId);
            }
            return null;
        }

        public static int? GetSpecialRoleType(this IEnumerable<Claim> claims)
        {
            string specialRoleType = claims.GetClaim(CustomClaimTypes.SpecialRoleType);
            if (!String.IsNullOrEmpty(specialRoleType))
            {
                return Convert.ToInt32(specialRoleType);
            }
            return null;
        }

        public static int? GetAdminWorkerId(this IEnumerable<Claim> claims)
        {
            string adminWorkerId = claims.GetClaim(CustomClaimTypes.AdminWorkerId);
            if (!String.IsNullOrEmpty(adminWorkerId))
            {
                return Convert.ToInt32(adminWorkerId);
            }
            return null;
        }

        public static bool? IsAdminOfCurrentOrganization(this IEnumerable<Claim> claims)
        {
            string isAdmin = claims.GetClaim(CustomClaimTypes.IsAdminOfOrg);
            if (!String.IsNullOrEmpty(isAdmin))
            {
                return Convert.ToBoolean(isAdmin);
            }
            return null;
        }


        public static string GetAdminAccount(this IEnumerable<Claim> claims)
        {
            return claims.GetClaim(CustomClaimTypes.AdminAccount);
        }
        public static string GetFirstName(this IEnumerable<Claim> claims)
        {
            return claims.GetClaim(CustomClaimTypes.FirstName);
        }
        public static string GetLastName(this IEnumerable<Claim> claims)
        {
            return claims.GetClaim(CustomClaimTypes.LastName);
        }
        public static string GetPatronymic(this IEnumerable<Claim> claims)
        {
            return claims.GetClaim(CustomClaimTypes.Patronymic);
        }

        public static string GetLegalEntityOgrn(this IEnumerable<Claim> claims)
        {
            return claims.GetClaim(CustomClaimTypes.CurrrentLegalEntityOGRN);
        }

        public static IEnumerable<SecurityAccessRule> GetSecurityRights(this IEnumerable<Claim> claims)
        {
            return claims.Where(cl => cl.Type == CustomClaimTypes.SecurityAccessRight).Select(cl => new SecurityAccessRule(cl.Value)).ToArray();
        }

        public static IEnumerable<Organization> GetOrganizations(this IEnumerable<Claim> claims)
        {
            return claims.Where(cl => cl.Type == CustomClaimTypes.LegalEntityInformation).Select(cl => new Organization(cl.Value)).ToArray();
        }

        public static IEnumerable<int> GetSettlements(this IEnumerable<Claim> claims)
        {
            var settlementList = claims.GetClaim(CustomClaimTypes.SettlementList);
            return settlementList.Split(',').Cast<int>().ToArray();
        }

    }
}
