using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfTestService
{
    //to debug with WCF Test Client set .Net Framework Advanced Services/Wcf Service/HTTP Activation in windows component      
    public class TestService : ITestService
    {
        public string TestMethod(string message, int parameter)
        {
            return $"{message} {parameter}";
        }
    }
}
