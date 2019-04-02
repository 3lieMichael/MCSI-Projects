using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Web.SP.Startup))]
namespace Web.SP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
