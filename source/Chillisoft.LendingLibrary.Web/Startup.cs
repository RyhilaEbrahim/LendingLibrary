using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chillisoft.LendingLibrary.Web.Startup))]
namespace Chillisoft.LendingLibrary.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
