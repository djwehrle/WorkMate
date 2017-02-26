using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WorkMate.Startup))]
namespace WorkMate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
