using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using ServiceProvider.Exceptions;

namespace WcfTestService
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string TestMethod(string message, int parameter);

        [OperationContract]
        Task DoActionAsync();

        [OperationContract]
        [FaultContract(typeof(CustomException))]
        Task DoAdministrationActionAsync();
    }
}
