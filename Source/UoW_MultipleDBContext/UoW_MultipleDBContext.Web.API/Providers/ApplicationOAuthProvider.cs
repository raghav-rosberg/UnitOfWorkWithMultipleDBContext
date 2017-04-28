using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Infrastructure;
using System.Text;
using System.Collections.Concurrent;
using Microsoft.Owin;
using System.Web;
using System.Configuration;
using UoW_MultipleDBContext.Data.UnitOfWork;
using System.Web.Http;

namespace UoW_MultipleDBContext.Web.WebApi.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;

        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var formData = await context.Request.ReadFormAsync() as IEnumerable<KeyValuePair<string, string[]>>;
            var clientId = formData.FirstOrDefault(x => x.Key == "clientId").Value[0];
            if (clientId != _publicClientId)
            {
                context.SetError("invalid_clientId", string.Format("Client '{0}' is not registered in the system.", clientId));
                return;
            }
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //Login user here
            //var user = await _unitOfWork.UserSecurityRepository.GetSingleEntity(
            //x => x.USERNAME.ToLower().Trim() == context.UserName.ToLower().Trim() && x.PASSWORD.Trim() == context.Password.Trim());
            ////Validate user
            //if (user == null)
            //{
            //    context.SetError("invalid_grant", "The user name or password is incorrect.");
            //    return;
            //}

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim("LoggedInTime", DateTime.Now.ToString()));
            var ticket = new AuthenticationTicket(identity, null);
            context.Validated(ticket);
        }
    }

    public class ApplicationRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var guid = Guid.NewGuid().ToString();

            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddSeconds(Convert.ToInt16(ConfigurationManager.AppSettings["RefreshTokenExpiryInSeconds"]))
            };
            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);

            _refreshTokens.TryAdd(guid, refreshTokenTicket);
            context.SetToken(guid);
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            AuthenticationTicket ticket;
            if (_refreshTokens.TryRemove(context.Token, out ticket))
            {
                context.SetTicket(ticket);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}