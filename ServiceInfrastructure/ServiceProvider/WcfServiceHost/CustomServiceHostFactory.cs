using System;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Autofac;
using Autofac.Integration.Wcf;
using ServiceProvider.WcfServiceHost.Logging;

namespace ServiceProvider.WcfServiceHost
{
    //add to markup wcfService.svc (View Markup) {Factory="ServiceProvider.WcfServiceHost.CustomServiceHostFactory"}
    public class CustomServiceHostFactory : ServiceHostFactory
    {

        protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            Type contractType = GetContractType(serviceType);
            var requestLogger = AutofacHostFactory.Container.Resolve<IRequestLogger>();
            var host = new CustomServiceHost(serviceType, requestLogger, baseAddresses);
            host.AddDependencyInjectionBehavior(contractType, AutofacHostFactory.Container);
            return host;
        }

        private static Type GetContractType(Type serviceType)
        {
            return serviceType.GetInterfaces()
                .FirstOrDefault(i => Attribute.IsDefined((MemberInfo) i, typeof(ServiceContractAttribute), false));
        }
    }
}