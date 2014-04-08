using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Minerva.Startup))]
namespace Minerva
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
