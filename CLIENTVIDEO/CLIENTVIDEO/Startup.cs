using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CLIENTVIDEO.Startup))]
namespace CLIENTVIDEO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
