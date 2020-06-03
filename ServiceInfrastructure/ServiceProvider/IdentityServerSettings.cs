using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceProvider
{
    public class IdentityServerSettings
    {
        public string IssuerEndpoint { get; set; }
        public string IssuerDnsName { get; set; }
        public string IssuerTokenName { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public string DefaultCertificate { get; set; }

        public string ServiceRealm { get; set; }

       
    }
}
