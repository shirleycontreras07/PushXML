using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PushXML.Startup))]
namespace PushXML
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
