using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Minerva.Entities;
using Minerva.Infrastructure;
using Owin;
using System;

namespace Minerva
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<UserManager<ApplicationUser>> UserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        static Startup()
        {
            PublicClientId = "self";

            UserManagerFactory = () => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>());
            
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");
/*
            var googleClientId = "762955059375-hr9c719v1dh1aasj98t31vtl538al83s.apps.googleusercontent.com";
            var googleClientSecret = "xvaYRQ8XVb8b_sdAY2PJLHnZ";

#if DEBUG
            googleClientId = "762955059375-o5sub78tqo75dg35eld1qjl7h92855jf.apps.googleusercontent.com";
            googleClientSecret = "6X0UhBlVU2FKXCWXkeGS6_ix";
#endif

            var googleOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = googleClientId,
                ClientSecret = googleClientSecret,
                CallbackPath = new PathString("/Account/ExternalLoginConfirmation"),
                
                /*Provider = new GoogleOAuth2AuthenticationProvider()
                { 
                }*/
            //};
            app.UseGoogleAuthentication(/*googleOptions*/);
        }
    }
}