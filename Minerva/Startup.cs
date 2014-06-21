using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(Minerva.Startup))]
[assembly: OwinStartupAttribute(typeof(Minerva.Startup))]
namespace Minerva
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // map signal r
            app.MapSignalR();
        }
    }
}
