using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Owin;
using UoW_MultipleDBContext.Web.WebApi.Providers;
using System.Web.Http;
using Microsoft.Owin.Security.Infrastructure;
using System.Configuration;
using UoW_MultipleDBContext.Data.UnitOfWork;

namespace UoW_MultipleDBContext.Web.WebApi
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/Token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromSeconds(Convert.ToInt16(ConfigurationManager.AppSettings["TokenExpiryInSeconds"])),
                RefreshTokenProvider = new ApplicationRefreshTokenProvider(),
                Provider = new ApplicationOAuthProvider(PublicClientId)
                //Provider = (IOAuthAuthorizationServerProvider)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IOAuthAuthorizationServerProvider))
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            //Configure webpi to allow cross orgin requests
            var config = new HttpConfiguration();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }
    }
}