using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfTestService
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string TestMethod(string message, int parameter);
    }
}
