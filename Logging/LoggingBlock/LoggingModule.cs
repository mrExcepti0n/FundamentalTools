using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using NLog;

namespace LoggingBlock
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LogRegistration.RegistryDebugLogger();
            builder.Register<ILogger>(
                (context, parameters) =>
                {
                    var namedParameter = parameters.FirstOrDefault(p => p is NamedParameter parameter && parameter.Name == "loggerName") as NamedParameter;

                    string loggerName = namedParameter?.Value != null && namedParameter.Value is string nps
                        ? nps
                        : "DefaultLogger";

                    return LogManager.GetLogger(loggerName);
                });

        }

        protected override void AttachToComponentRegistration(IComponentRegistryBuilder componentRegistry, IComponentRegistration registration)
        {
            registration.Preparing += (sender, e) =>
            {
                Type limitType = e.Component.Activator.LimitType;
                e.Parameters = e.Parameters.Union(new[] {
                    new ResolvedParameter((pi, c) => pi.ParameterType == typeof(ILogger),
                        (pi, c) =>
                        {
                            List<Parameter> parameters = new List<Parameter>();
                            if (!string.IsNullOrWhiteSpace(limitType?.FullName))
                            {
                                parameters.Add(new NamedParameter("loggerName", limitType.FullName));
                            }
                            return c.Resolve<ILogger>(parameters);
                        })
                });
            };
        }
    }
}
