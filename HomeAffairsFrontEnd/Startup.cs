using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeAffairsFrontEnd.Startup))]
namespace HomeAffairsFrontEnd
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
