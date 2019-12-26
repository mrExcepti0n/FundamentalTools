using SecurityBlock.Abstraction.Model;
using System;

namespace SecurityBlock.Abstraction.IdentityProvider
{
    public class SecurityAccessRule
    {
        public SecurityAccessRule(SecurityAccessObjectEnum accessObject, SecurityAccessActionEnum action)
        {
            AccessObject = accessObject;
            Action = action;
        }

        public SecurityAccessRule(string claimValue)
        {
            var splittedResult = claimValue.Split(',');  

            AccessObject = (SecurityAccessObjectEnum)Enum.Parse(typeof(SecurityAccessObjectEnum), splittedResult[0]);
            Action = (SecurityAccessActionEnum)Enum.Parse(typeof(SecurityAccessActionEnum), splittedResult[1]);
        }

        public SecurityAccessRule()
        {

        }
        public SecurityAccessObjectEnum AccessObject;
        public SecurityAccessActionEnum Action;

    }
}