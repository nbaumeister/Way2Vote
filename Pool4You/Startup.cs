using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Pool4You.Startup))]
namespace Pool4You
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
