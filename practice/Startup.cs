using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(practice.Startup))]
namespace practice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
