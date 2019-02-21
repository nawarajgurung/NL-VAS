using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NL_VAS.Startup))]
namespace NL_VAS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
