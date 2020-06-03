using Autofac.Integration.Wcf;
using System;
using System.Configuration;
using Autofac;
using LoggingBlock;
using NLog;
using SecurityBlock;
using SecurityBlock.Abstraction.IdentityProvider;
using SecurityBlock.Abstraction.Model;
using ServiceProvider.WcfServiceHost;
using ServiceProvider.WcfServiceHost.Logging;

namespace WcfTestService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TestService>().As<ITestService>();

            builder.RegisterType<WcfRequestLogger>().As<IRequestLogger>();
           

#if DEBUG
            builder.RegisterType<RequestLoggerOutputTarget>().As<IRequestLoggerTarget>();
#else
            builder.RegisterType<RequestLoggerDbTarget>().As<IRequestLoggerTarget>()
                .WithParameter(((info, context) => info.Position == 0 && info.ParameterType == typeof(string)), (info, context) 
                    => ConfigurationManager.ConnectionStrings["LogSqlConnection"].ConnectionString);
#endif

            builder.RegistryIdentityProvider();
            builder.RegisterModule<LoggingModule>();

            IContainer container = builder.Build();
            AutofacHostFactory.Container = container;

            RegistryIdentityFactory(container);
        }

        private void RegistryIdentityFactory(IContainer container)
        {
            IdentityProviderFactory.Init(container.Resolve<IIdentityProvider>());
        }
    }


    public static class ContainerBuilderExtensions
    {
        public static void RegistryIdentityProvider(this ContainerBuilder builder)
        {
            builder.Register<IIdentityProvider>(ip => new TestIdentityProvider()
                    .WithSecurityAccessRights(new SecurityAccessRule(SecurityAccessObjectEnum.Agreement, SecurityAccessActionEnum.R)));
        }
    }
}