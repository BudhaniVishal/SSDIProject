using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SSDI_SPILELApplication.Startup))]
namespace SSDI_SPILELApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
