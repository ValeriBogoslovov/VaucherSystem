using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VaucherSystem.Web.Startup))]
namespace VaucherSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
