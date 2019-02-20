using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(salesforceInt1.Startup))]
namespace salesforceInt1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
