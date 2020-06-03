using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider.WcfServiceHost
{
    public class WcfHostSettings
    {
        public WcfHostSettings(string connectionString)
        {
            LogSqlConnectionString = connectionString;
        }

        public string LogSqlConnectionString { get;}
    }
}
