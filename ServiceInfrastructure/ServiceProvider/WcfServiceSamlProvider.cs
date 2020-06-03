using System;
using System.Collections.Generic;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel;
using System.ServiceModel.Security;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Thinktecture.IdentityModel.Constants;
using Thinktecture.IdentityModel.Extensions;
using Thinktecture.IdentityModel.WSTrust;

namespace ServiceProvider
{
    public class WcfServiceSamlProvider : IServiceProvider
    {
        public WcfServiceSamlProvider(IdentityServerSettings settings)
        {
            _settings = settings;
            _idpAddress = new Uri(_settings.IssuerEndpoint);
        }

        private readonly IdentityServerSettings _settings;

        private List<Claim> _claims => ClaimsPrincipal.Current.Claims.ToList();
        private readonly Uri _idpAddress;

        public TService GetService<TService>() where TService : class
        {
            var channelFactory = new ChannelFactory<TService>("*");
            var securityToken = RequestSecurityToken();
            return channelFactory.CreateChannelWithIssuedToken(securityToken);
        }


        private SecurityToken RequestSecurityToken()
        {
            var bootstrapContext = ClaimsPrincipal.Current.Identities.FirstOrDefault()?.BootstrapContext as BootstrapContext;
            var saml2SecurityToken = bootstrapContext?.SecurityToken;
            if (saml2SecurityToken == null)
            {
                return RequestGenericXmlSecurityToken();
            }
            var sbToken = new StringBuilder();
            using (var xmlWriter = XmlWriter.Create(sbToken))
            {
                new Saml2SecurityTokenHandler(new SamlSecurityTokenRequirement()).WriteToken(xmlWriter, saml2SecurityToken);
            }
            var theXml = sbToken.ToString();
            var xmlToken = new GenericXmlSecurityToken(
                XElement.Parse(theXml).ToXmlElement(),
                null, 
                saml2SecurityToken.ValidFrom,
                saml2SecurityToken.ValidTo,
                null,
                null,
                null);
            return xmlToken;
        }

        private SecurityToken RequestGenericXmlSecurityToken()
        {
            var factory = InitializeChannelFactory();
            var token = GetSecurityToken();
            var requestSecurityToken = new RequestSecurityToken
            {
                RequestType = WSTrust13Constants.RequestTypes.Issue,
                KeyType = WSTrust13Constants.KeyTypes.Bearer,
                AppliesTo = new EndpointReference(_settings.ServiceRealm),
                ActAs = new SecurityTokenElement(token),
                TokenType = TokenTypes.OasisWssSaml2TokenProfile11,
            };

            var channel = factory.CreateChannelWithIssuedToken(token);

            SecurityToken securityToken = channel.Issue(requestSecurityToken);
            return securityToken;
        }

        private WSTrustChannelFactory InitializeChannelFactory()
        {
            var factory = new WSTrustChannelFactory(new UserNameWSTrustBinding(SecurityMode.Message),
                new EndpointAddress(_idpAddress, new DnsEndpointIdentity(_settings.IssuerDnsName)))
            {
                TrustVersion = TrustVersion.WSTrust13
            };
            if (factory.Credentials == null) return factory;
            factory.Credentials.SupportInteractive = false;
            factory.Credentials.UserName.UserName = _settings.UserName;
            factory.Credentials.UserName.Password = _settings.Password;
            factory.Credentials.ServiceCertificate.SetDefaultCertificate(StoreLocation.LocalMachine, StoreName.My,
                X509FindType.FindByThumbprint, _settings.DefaultCertificate);

            return factory;
        }

        private SecurityToken GetSecurityToken()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            var data = store.Certificates.Find(X509FindType.FindByThumbprint, _settings.DefaultCertificate, false);
            var certificate = data[0];

            var saml2SecurityTokenHandler = new Saml2SecurityTokenHandler();
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                TokenIssuerName = _settings.IssuerTokenName,
                Subject = new ClaimsIdentity(_claims),
                Token = new X509SecurityToken(certificate),
                SigningCredentials = new X509SigningCredentials(certificate)
            };
            var securityToken = saml2SecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return securityToken;
        }
    }
}
