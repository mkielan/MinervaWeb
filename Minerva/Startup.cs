using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Minevra.Startup))]
namespace Minevra
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
