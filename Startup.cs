using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TracersCafe.Startup))]
namespace TracersCafe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
