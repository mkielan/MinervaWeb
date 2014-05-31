using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace Minerva
{
    public partial class Startup
    {
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