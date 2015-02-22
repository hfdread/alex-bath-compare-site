using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_BathCompareSIte.Startup))]
namespace MVC_BathCompareSIte
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
