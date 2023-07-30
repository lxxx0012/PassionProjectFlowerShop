using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProjectFlowerShop.Startup))]
namespace PassionProjectFlowerShop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
