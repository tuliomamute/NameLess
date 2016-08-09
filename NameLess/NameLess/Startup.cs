using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NameLess.Startup))]
namespace NameLess
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
