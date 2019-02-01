using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Schoolio.Startup))]
namespace Schoolio
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
