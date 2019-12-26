using SecurityBlock.Abstraction.Model;
using SecurityBlock.SecurityProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SecurityBlock.Tests
{
    public class SomeClass
    {

        [CustomPrincipalPermission(SecurityAction.Demand, AccessObject = SecurityAccessObjectEnum.Agreement, AccessAction = SecurityAccessActionEnum.RW)]
        public object SomeMethod()
        {
            return 5;
        }


        [CustomPrincipalPermission(SecurityAction.Demand, AccessObject = SecurityAccessObjectEnum.Agreement, AccessAction = SecurityAccessActionEnum.RW)]
        [CustomPrincipalPermission(SecurityAction.Demand, AccessObject = SecurityAccessObjectEnum.Report, AccessAction = SecurityAccessActionEnum.RW)]
        public object SomeMethod2()
        {
            return 6;
        }
    }
}
