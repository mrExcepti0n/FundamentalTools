using Autofac.Integration.Wcf;
using System;
using System.Configuration;
using Autofac;
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


            IContainer container = builder.Build();
            AutofacHostFactory.Container = container;
        }

    }
}