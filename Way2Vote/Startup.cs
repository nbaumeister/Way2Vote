using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Way2Vote.Startup))]
namespace Way2Vote
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
