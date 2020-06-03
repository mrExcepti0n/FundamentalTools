using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using NLog;
using SecurityBlock.Abstraction.Model;
using SecurityBlock.SecurityProvider;

namespace WcfTestService
{
    //to debug with WCF Test Client set .Net Framework Advanced Services/Wcf Service/HTTP Activation in windows component      
    public class TestService : ITestService
    {
        private readonly ILogger _logger;
        public TestService(ILogger logger)
        {
            _logger = logger;
        }

        public string TestMethod(string message, int parameter)
        {
            return $"{message} {parameter}";
        }


        [CustomPrincipalPermission(SecurityAction.Demand, AccessObject = SecurityAccessObjectEnum.Agreement, AccessAction = SecurityAccessActionEnum.R)]
        public async Task DoActionAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }

        [CustomPrincipalPermission(SecurityAction.Demand, AccessObject = SecurityAccessObjectEnum.Administration, AccessAction = SecurityAccessActionEnum.R)]
        public async Task DoAdministrationActionAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
