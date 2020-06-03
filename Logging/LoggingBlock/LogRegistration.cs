using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace LoggingBlock
{
    public class LogRegistration
    {
        public static void RegistryDebugLogger()
        {
            LoggingConfiguration loggingConfiguration = LogManager.Configuration ?? new LoggingConfiguration();

            var debugTarget = GetDebugTarget();
            loggingConfiguration.AddTarget(debugTarget);
            var debugLoggingRule = new LoggingRule("*", LogLevel.Debug, debugTarget);
            loggingConfiguration.LoggingRules.Add(debugLoggingRule);

            LogManager.Configuration = loggingConfiguration;
        }

        private static Target GetDebugTarget()
        {
            return new OutputDebugStringTarget("debugTarget");
        }


    }
}
